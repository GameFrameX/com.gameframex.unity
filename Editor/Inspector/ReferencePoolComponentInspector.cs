// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameFrameX.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    [CustomEditor(typeof(ReferencePoolComponent))]
    internal sealed class ReferencePoolComponentInspector : GameFrameworkInspector
    {
        private readonly Dictionary<string, List<ReferencePoolInfo>> m_ReferencePoolInfos = new Dictionary<string, List<ReferencePoolInfo>>(StringComparer.Ordinal);
        private readonly HashSet<string> m_OpenedItems = new HashSet<string>();

        private SerializedProperty m_EnableStrictCheck = null;

        private bool m_ShowFullClassName = false;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            ReferencePoolComponent t = (ReferencePoolComponent)target;

            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                bool enableStrictCheck = EditorGUILayout.Toggle("Enable Strict Check", t.EnableStrictCheck);
                if (enableStrictCheck != t.EnableStrictCheck)
                {
                    t.EnableStrictCheck = enableStrictCheck;
                }

                EditorGUILayout.LabelField("Reference Pool Count", ReferencePool.Count.ToString());
                m_ShowFullClassName = EditorGUILayout.Toggle("Show Full Class Name", m_ShowFullClassName);
                m_ReferencePoolInfos.Clear();
                ReferencePoolInfo[] referencePoolInfos = ReferencePool.GetAllReferencePoolInfos();
                foreach (ReferencePoolInfo referencePoolInfo in referencePoolInfos)
                {
                    string assemblyName = referencePoolInfo.Type.Assembly.GetName().Name;
                    List<ReferencePoolInfo> results = null;
                    if (!m_ReferencePoolInfos.TryGetValue(assemblyName, out results))
                    {
                        results = new List<ReferencePoolInfo>();
                        m_ReferencePoolInfos.Add(assemblyName, results);
                    }

                    results.Add(referencePoolInfo);
                }

                foreach (KeyValuePair<string, List<ReferencePoolInfo>> assemblyReferencePoolInfo in m_ReferencePoolInfos)
                {
                    bool lastState = m_OpenedItems.Contains(assemblyReferencePoolInfo.Key);
                    bool currentState = EditorGUILayout.Foldout(lastState, assemblyReferencePoolInfo.Key);
                    if (currentState != lastState)
                    {
                        if (currentState)
                        {
                            m_OpenedItems.Add(assemblyReferencePoolInfo.Key);
                        }
                        else
                        {
                            m_OpenedItems.Remove(assemblyReferencePoolInfo.Key);
                        }
                    }

                    if (currentState)
                    {
                        EditorGUILayout.BeginVertical("box");
                        {
                            EditorGUILayout.LabelField(m_ShowFullClassName ? "Full Class Name" : "Class Name", "Unused\tUsing\tAcquire\tRelease\tAdd\tRemove");
                            assemblyReferencePoolInfo.Value.Sort(Comparison);
                            foreach (ReferencePoolInfo referencePoolInfo in assemblyReferencePoolInfo.Value)
                            {
                                DrawReferencePoolInfo(referencePoolInfo);
                            }

                            if (GUILayout.Button("Export CSV Data"))
                            {
                                string exportFileName = EditorUtility.SaveFilePanel("Export CSV Data", string.Empty, Utility.Text.Format("Reference Pool Data - {0}.csv", assemblyReferencePoolInfo.Key), string.Empty);
                                if (!string.IsNullOrEmpty(exportFileName))
                                {
                                    try
                                    {
                                        int index = 0;
                                        string[] data = new string[assemblyReferencePoolInfo.Value.Count + 1];
                                        data[index++] = "Class Name,Full Class Name,Unused,Using,Acquire,Release,Add,Remove";
                                        foreach (ReferencePoolInfo referencePoolInfo in assemblyReferencePoolInfo.Value)
                                        {
                                            data[index++] = Utility.Text.Format("{0},{1},{2},{3},{4},{5},{6},{7}", referencePoolInfo.Type.Name, referencePoolInfo.Type.FullName, referencePoolInfo.UnusedReferenceCount, referencePoolInfo.UsingReferenceCount, referencePoolInfo.AcquireReferenceCount, referencePoolInfo.ReleaseReferenceCount, referencePoolInfo.AddReferenceCount, referencePoolInfo.RemoveReferenceCount);
                                        }

                                        File.WriteAllLines(exportFileName, data, Encoding.UTF8);
                                        Debug.Log(Utility.Text.Format("Export reference pool CSV data to '{0}' success.", exportFileName));
                                    }
                                    catch (Exception exception)
                                    {
                                        Debug.LogError(Utility.Text.Format("Export reference pool CSV data to '{0}' failure, exception is '{1}'.", exportFileName, exception));
                                    }
                                }
                            }
                        }
                        EditorGUILayout.EndVertical();

                        EditorGUILayout.Separator();
                    }
                }
            }
            else
            {
                EditorGUILayout.PropertyField(m_EnableStrictCheck);
            }

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        private void OnEnable()
        {
            m_EnableStrictCheck = serializedObject.FindProperty("m_EnableStrictCheck");
        }

        private void DrawReferencePoolInfo(ReferencePoolInfo referencePoolInfo)
        {
            EditorGUILayout.LabelField(m_ShowFullClassName ? referencePoolInfo.Type.FullName : referencePoolInfo.Type.Name, Utility.Text.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", referencePoolInfo.UnusedReferenceCount, referencePoolInfo.UsingReferenceCount, referencePoolInfo.AcquireReferenceCount, referencePoolInfo.ReleaseReferenceCount, referencePoolInfo.AddReferenceCount, referencePoolInfo.RemoveReferenceCount));
        }

        private int Comparison(ReferencePoolInfo a, ReferencePoolInfo b)
        {
            if (m_ShowFullClassName)
            {
                return a.Type.FullName.CompareTo(b.Type.FullName);
            }
            else
            {
                return a.Type.Name.CompareTo(b.Type.Name);
            }
        }
    }
}