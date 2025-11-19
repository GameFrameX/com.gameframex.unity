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

using GameFrameX.ObjectPool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameFrameX.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    [CustomEditor(typeof(ObjectPoolComponent))]
    internal sealed class ObjectPoolComponentInspector : ComponentTypeComponentInspector
    {
        private readonly HashSet<string> m_OpenedItems = new HashSet<string>();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("Available during runtime only.", MessageType.Info);
                return;
            }

            ObjectPoolComponent t = (ObjectPoolComponent)target;

            if (IsPrefabInHierarchy(t.gameObject))
            {
                EditorGUILayout.LabelField("Object Pool Count", t.Count.ToString());

                ObjectPoolBase[] objectPools = t.GetAllObjectPools(true);
                foreach (ObjectPoolBase objectPool in objectPools)
                {
                    DrawObjectPool(objectPool);
                }
            }

            Repaint();
        }

        protected override void RefreshTypeNames()
        {
            RefreshComponentTypeNames(typeof(IObjectPoolManager));
        }

        private void DrawObjectPool(ObjectPoolBase objectPool)
        {
            bool lastState = m_OpenedItems.Contains(objectPool.FullName);
            bool currentState = EditorGUILayout.Foldout(lastState, objectPool.FullName);
            if (currentState != lastState)
            {
                if (currentState)
                {
                    m_OpenedItems.Add(objectPool.FullName);
                }
                else
                {
                    m_OpenedItems.Remove(objectPool.FullName);
                }
            }

            if (currentState)
            {
                EditorGUILayout.BeginVertical("box");
                {
                    EditorGUILayout.LabelField("Name", objectPool.Name);
                    EditorGUILayout.LabelField("Type", objectPool.ObjectType.FullName);
                    EditorGUILayout.LabelField("Auto Release Interval", objectPool.AutoReleaseInterval.ToString());
                    EditorGUILayout.LabelField("Capacity", objectPool.Capacity.ToString());
                    EditorGUILayout.LabelField("Used Count", objectPool.Count.ToString());
                    EditorGUILayout.LabelField("Can Release Count", objectPool.CanReleaseCount.ToString());
                    EditorGUILayout.LabelField("Expire Time", objectPool.ExpireTime.ToString());
                    EditorGUILayout.LabelField("Priority", objectPool.Priority.ToString());
                    ObjectInfo[] objectInfos = objectPool.GetAllObjectInfos();
                    if (objectInfos.Length > 0)
                    {
                        EditorGUILayout.LabelField("Name", objectPool.AllowMultiSpawn ? "Locked\tCount\tFlag\tPriority\tLast Use Time" : "Locked\tIn Use\tFlag\tPriority\tLast Use Time");
                        foreach (ObjectInfo objectInfo in objectInfos)
                        {
                            EditorGUILayout.LabelField(string.IsNullOrEmpty(objectInfo.Name) ? "<None>" : objectInfo.Name,
                                                       objectPool.AllowMultiSpawn
                                                           ? Utility.Text.Format("{0}\t{1}\t{2}\t{3}\t{4:yyyy-MM-dd HH:mm:ss}", objectInfo.Locked, objectInfo.SpawnCount, objectInfo.CustomCanReleaseFlag, objectInfo.Priority, objectInfo.LastUseTime.ToLocalTime())
                                                           : Utility.Text.Format("{0}\t{1}\t{2}\t{3}\t{4:yyyy-MM-dd HH:mm:ss}", objectInfo.Locked, objectInfo.IsInUse, objectInfo.CustomCanReleaseFlag, objectInfo.Priority, objectInfo.LastUseTime.ToLocalTime()));
                        }

                        if (GUILayout.Button("Release"))
                        {
                            objectPool.Release();
                        }

                        if (GUILayout.Button("Release All Unused"))
                        {
                            objectPool.ReleaseAllUnused();
                        }

                        if (GUILayout.Button("Export CSV Data"))
                        {
                            string exportFileName = EditorUtility.SaveFilePanel("Export CSV Data", string.Empty, Utility.Text.Format("Object Pool Data - {0}.csv", objectPool.Name), string.Empty);
                            if (!string.IsNullOrEmpty(exportFileName))
                            {
                                try
                                {
                                    int index = 0;
                                    string[] data = new string[objectInfos.Length + 1];
                                    data[index++] = Utility.Text.Format("Name,Locked,{0},Custom Can Release Flag,Priority,Last Use Time", objectPool.AllowMultiSpawn ? "Count" : "In Use");
                                    foreach (ObjectInfo objectInfo in objectInfos)
                                    {
                                        data[index++] = objectPool.AllowMultiSpawn
                                                            ? Utility.Text.Format("{0},{1},{2},{3},{4},{5:yyyy-MM-dd HH:mm:ss}", objectInfo.Name, objectInfo.Locked, objectInfo.SpawnCount, objectInfo.CustomCanReleaseFlag, objectInfo.Priority, objectInfo.LastUseTime.ToLocalTime())
                                                            : Utility.Text.Format("{0},{1},{2},{3},{4},{5:yyyy-MM-dd HH:mm:ss}", objectInfo.Name, objectInfo.Locked, objectInfo.IsInUse, objectInfo.CustomCanReleaseFlag, objectInfo.Priority, objectInfo.LastUseTime.ToLocalTime());
                                    }

                                    File.WriteAllLines(exportFileName, data, Encoding.UTF8);
                                    Debug.Log(Utility.Text.Format("Export object pool CSV data to '{0}' success.", exportFileName));
                                }
                                catch (Exception exception)
                                {
                                    Debug.LogError(Utility.Text.Format("Export object pool CSV data to '{0}' failure, exception is '{1}'.", exportFileName, exception));
                                }
                            }
                        }
                    }
                    else
                    {
                        GUILayout.Label("Object Pool is Empty ...");
                    }
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.Separator();
            }
        }
    }
}