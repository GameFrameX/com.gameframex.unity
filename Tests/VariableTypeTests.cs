using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class VariableTypeTests
    {
        [SetUp]
        public void SetUp()
        {
            ReferencePool.ClearAll();
        }

        [TearDown]
        public void TearDown()
        {
            ReferencePool.ClearAll();
        }

        #region VarBoolean

        [Test]
        public void VarBoolean_DefaultValue_IsFalse()
        {
            var v = new VarBoolean();
            Assert.IsFalse(v.Value);
        }

        [Test]
        public void VarBoolean_Value_GetSet()
        {
            var v = new VarBoolean();
            v.Value = true;
            Assert.IsTrue(v.Value);
        }

        [Test]
        public void VarBoolean_Clear_ResetsToDefault()
        {
            var v = new VarBoolean();
            v.Value = true;
            v.Clear();
            Assert.IsFalse(v.Value);
        }

        [Test]
        public void VarBoolean_Type_ReturnsBool()
        {
            var v = new VarBoolean();
            Assert.AreEqual(typeof(bool), v.Type);
        }

        [Test]
        public void VarBoolean_SetValue_CorrectType()
        {
            var v = new VarBoolean();
            v.SetValue(true);
            Assert.IsTrue(v.Value);
        }

        [Test]
        public void VarBoolean_SetValue_WrongType_Throws()
        {
            var v = new VarBoolean();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("not a bool"));
        }

        [Test]
        public void VarBoolean_SetValue_Null_SetsDefault()
        {
            var v = new VarBoolean();
            v.Value = true;
            v.SetValue(null);
            Assert.IsFalse(v.Value);
        }

        [Test]
        public void VarBoolean_GetValue_ReturnsBoxed()
        {
            var v = new VarBoolean();
            v.Value = true;
            Assert.AreEqual(true, v.GetValue());
        }

        [Test]
        public void VarBoolean_ToString_True()
        {
            var v = new VarBoolean();
            v.Value = true;
            Assert.AreEqual(true.ToString(), v.ToString());
        }

        [Test]
        public void VarBoolean_ToString_False()
        {
            var v = new VarBoolean();
            v.Value = false;
            Assert.AreEqual(false.ToString(), v.ToString());
        }

        [Test]
        public void VarBoolean_ImplicitFromBool()
        {
            VarBoolean v = true;
            Assert.IsTrue(v.Value);
        }

        [Test]
        public void VarBoolean_ImplicitToBool()
        {
            var v = new VarBoolean();
            v.Value = true;
            bool result = v;
            Assert.IsTrue(result);
        }

        [Test]
        public void VarBoolean_ReferencePool_AcquireRelease()
        {
            var v = ReferencePool.Acquire<VarBoolean>();
            v.Value = true;
            ReferencePool.Release(v);
            var v2 = ReferencePool.Acquire<VarBoolean>();
            Assert.IsFalse(v2.Value);
        }

        #endregion

        #region VarByte

        [Test]
        public void VarByte_DefaultValue_IsZero()
        {
            var v = new VarByte();
            Assert.AreEqual((byte)0, v.Value);
        }

        [Test]
        public void VarByte_Value_GetSet()
        {
            var v = new VarByte();
            v.Value = 200;
            Assert.AreEqual((byte)200, v.Value);
        }

        [Test]
        public void VarByte_Clear_ResetsToDefault()
        {
            var v = new VarByte();
            v.Value = 100;
            v.Clear();
            Assert.AreEqual((byte)0, v.Value);
        }

        [Test]
        public void VarByte_Type_ReturnsByte()
        {
            var v = new VarByte();
            Assert.AreEqual(typeof(byte), v.Type);
        }

        [Test]
        public void VarByte_SetValue_WrongType_Throws()
        {
            var v = new VarByte();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("not a byte"));
        }

        [Test]
        public void VarByte_ImplicitFromByte()
        {
            VarByte v = (byte)42;
            Assert.AreEqual((byte)42, v.Value);
        }

        [Test]
        public void VarByte_ImplicitToByte()
        {
            var v = new VarByte();
            v.Value = 42;
            byte result = v;
            Assert.AreEqual((byte)42, result);
        }

        [Test]
        public void VarByte_ReferencePool_AcquireRelease()
        {
            var v = ReferencePool.Acquire<VarByte>();
            v.Value = 99;
            ReferencePool.Release(v);
            var v2 = ReferencePool.Acquire<VarByte>();
            Assert.AreEqual((byte)0, v2.Value);
        }

        #endregion

        #region VarSByte

        [Test]
        public void VarSByte_DefaultValue_IsZero()
        {
            var v = new VarSByte();
            Assert.AreEqual((sbyte)0, v.Value);
        }

        [Test]
        public void VarSByte_Value_GetSet()
        {
            var v = new VarSByte();
            v.Value = -50;
            Assert.AreEqual((sbyte)(-50), v.Value);
        }

        [Test]
        public void VarSByte_Clear_ResetsToDefault()
        {
            var v = new VarSByte();
            v.Value = -10;
            v.Clear();
            Assert.AreEqual((sbyte)0, v.Value);
        }

        [Test]
        public void VarSByte_Type_ReturnsSByte()
        {
            var v = new VarSByte();
            Assert.AreEqual(typeof(sbyte), v.Type);
        }

        [Test]
        public void VarSByte_SetValue_WrongType_Throws()
        {
            var v = new VarSByte();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(1.0));
        }

        [Test]
        public void VarSByte_ImplicitFromSByte()
        {
            VarSByte v = (sbyte)(-42);
            Assert.AreEqual((sbyte)(-42), v.Value);
        }

        [Test]
        public void VarSByte_ImplicitToSByte()
        {
            var v = new VarSByte();
            v.Value = -42;
            sbyte result = v;
            Assert.AreEqual((sbyte)(-42), result);
        }

        [Test]
        public void VarSByte_ReferencePool_AcquireRelease()
        {
            var v = ReferencePool.Acquire<VarSByte>();
            v.Value = -1;
            ReferencePool.Release(v);
            var v2 = ReferencePool.Acquire<VarSByte>();
            Assert.AreEqual((sbyte)0, v2.Value);
        }

        #endregion

        #region VarChar

        [Test]
        public void VarChar_DefaultValue_IsNullChar()
        {
            var v = new VarChar();
            Assert.AreEqual('\0', v.Value);
        }

        [Test]
        public void VarChar_Value_GetSet()
        {
            var v = new VarChar();
            v.Value = 'A';
            Assert.AreEqual('A', v.Value);
        }

        [Test]
        public void VarChar_Clear_ResetsToDefault()
        {
            var v = new VarChar();
            v.Value = 'Z';
            v.Clear();
            Assert.AreEqual('\0', v.Value);
        }

        [Test]
        public void VarChar_Type_ReturnsChar()
        {
            var v = new VarChar();
            Assert.AreEqual(typeof(char), v.Type);
        }

        [Test]
        public void VarChar_SetValue_WrongType_Throws()
        {
            var v = new VarChar();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(123));
        }

        [Test]
        public void VarChar_ImplicitFromChar()
        {
            VarChar v = 'X';
            Assert.AreEqual('X', v.Value);
        }

        [Test]
        public void VarChar_ImplicitToChar()
        {
            var v = new VarChar();
            v.Value = 'X';
            char result = v;
            Assert.AreEqual('X', result);
        }

        #endregion

        #region VarInt16

        [Test]
        public void VarInt16_DefaultValue_IsZero()
        {
            var v = new VarInt16();
            Assert.AreEqual((short)0, v.Value);
        }

        [Test]
        public void VarInt16_Value_GetSet()
        {
            var v = new VarInt16();
            v.Value = -12345;
            Assert.AreEqual((short)(-12345), v.Value);
        }

        [Test]
        public void VarInt16_Clear_ResetsToDefault()
        {
            var v = new VarInt16();
            v.Value = 100;
            v.Clear();
            Assert.AreEqual((short)0, v.Value);
        }

        [Test]
        public void VarInt16_Type_ReturnsInt16()
        {
            var v = new VarInt16();
            Assert.AreEqual(typeof(short), v.Type);
        }

        [Test]
        public void VarInt16_SetValue_WrongType_Throws()
        {
            var v = new VarInt16();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("bad"));
        }

        [Test]
        public void VarInt16_ImplicitFromShort()
        {
            VarInt16 v = -1000;
            Assert.AreEqual((short)(-1000), v.Value);
        }

        [Test]
        public void VarInt16_ImplicitToShort()
        {
            var v = new VarInt16();
            v.Value = -1000;
            short result = v;
            Assert.AreEqual((short)(-1000), result);
        }

        #endregion

        #region VarUInt16

        [Test]
        public void VarUInt16_DefaultValue_IsZero()
        {
            var v = new VarUInt16();
            Assert.AreEqual((ushort)0, v.Value);
        }

        [Test]
        public void VarUInt16_Value_GetSet()
        {
            var v = new VarUInt16();
            v.Value = 60000;
            Assert.AreEqual((ushort)60000, v.Value);
        }

        [Test]
        public void VarUInt16_Clear_ResetsToDefault()
        {
            var v = new VarUInt16();
            v.Value = 100;
            v.Clear();
            Assert.AreEqual((ushort)0, v.Value);
        }

        [Test]
        public void VarUInt16_Type_ReturnsUInt16()
        {
            var v = new VarUInt16();
            Assert.AreEqual(typeof(ushort), v.Type);
        }

        [Test]
        public void VarUInt16_SetValue_WrongType_Throws()
        {
            var v = new VarUInt16();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(1.5));
        }

        [Test]
        public void VarUInt16_ImplicitFromUShort()
        {
            VarUInt16 v = 50000;
            Assert.AreEqual((ushort)50000, v.Value);
        }

        [Test]
        public void VarUInt16_ImplicitToUShort()
        {
            var v = new VarUInt16();
            v.Value = 50000;
            ushort result = v;
            Assert.AreEqual((ushort)50000, result);
        }

        #endregion

        #region VarInt32

        [Test]
        public void VarInt32_DefaultValue_IsZero()
        {
            var v = new VarInt32();
            Assert.AreEqual(0, v.Value);
        }

        [Test]
        public void VarInt32_Value_GetSet()
        {
            var v = new VarInt32();
            v.Value = -999999;
            Assert.AreEqual(-999999, v.Value);
        }

        [Test]
        public void VarInt32_Clear_ResetsToDefault()
        {
            var v = new VarInt32();
            v.Value = 42;
            v.Clear();
            Assert.AreEqual(0, v.Value);
        }

        [Test]
        public void VarInt32_Type_ReturnsInt32()
        {
            var v = new VarInt32();
            Assert.AreEqual(typeof(int), v.Type);
        }

        [Test]
        public void VarInt32_SetValue_CorrectType()
        {
            var v = new VarInt32();
            v.SetValue(123);
            Assert.AreEqual(123, v.Value);
        }

        [Test]
        public void VarInt32_SetValue_WrongType_Throws()
        {
            var v = new VarInt32();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("not an int"));
        }

        [Test]
        public void VarInt32_GetValue_ReturnsBoxed()
        {
            var v = new VarInt32();
            v.Value = 42;
            Assert.AreEqual(42, v.GetValue());
        }

        [Test]
        public void VarInt32_ToString_ReturnsStringRepresentation()
        {
            var v = new VarInt32();
            v.Value = 42;
            Assert.AreEqual("42", v.ToString());
        }

        [Test]
        public void VarInt32_ImplicitFromInt()
        {
            VarInt32 v = 42;
            Assert.AreEqual(42, v.Value);
        }

        [Test]
        public void VarInt32_ImplicitToInt()
        {
            var v = new VarInt32();
            v.Value = 42;
            int result = v;
            Assert.AreEqual(42, result);
        }

        [Test]
        public void VarInt32_ReferencePool_AcquireRelease()
        {
            var v = ReferencePool.Acquire<VarInt32>();
            v.Value = 100;
            ReferencePool.Release(v);
            var v2 = ReferencePool.Acquire<VarInt32>();
            Assert.AreEqual(0, v2.Value);
        }

        #endregion

        #region VarUInt32

        [Test]
        public void VarUInt32_DefaultValue_IsZero()
        {
            var v = new VarUInt32();
            Assert.AreEqual((uint)0, v.Value);
        }

        [Test]
        public void VarUInt32_Value_GetSet()
        {
            var v = new VarUInt32();
            v.Value = 4000000000;
            Assert.AreEqual((uint)4000000000, v.Value);
        }

        [Test]
        public void VarUInt32_Clear_ResetsToDefault()
        {
            var v = new VarUInt32();
            v.Value = 100;
            v.Clear();
            Assert.AreEqual((uint)0, v.Value);
        }

        [Test]
        public void VarUInt32_Type_ReturnsUInt32()
        {
            var v = new VarUInt32();
            Assert.AreEqual(typeof(uint), v.Type);
        }

        [Test]
        public void VarUInt32_SetValue_WrongType_Throws()
        {
            var v = new VarUInt32();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(-1));
        }

        [Test]
        public void VarUInt32_ImplicitFromUInt()
        {
            VarUInt32 v = 3000000000;
            Assert.AreEqual((uint)3000000000, v.Value);
        }

        [Test]
        public void VarUInt32_ImplicitToUInt()
        {
            var v = new VarUInt32();
            v.Value = 3000000000;
            uint result = v;
            Assert.AreEqual((uint)3000000000, result);
        }

        #endregion

        #region VarInt64

        [Test]
        public void VarInt64_DefaultValue_IsZero()
        {
            var v = new VarInt64();
            Assert.AreEqual((long)0, v.Value);
        }

        [Test]
        public void VarInt64_Value_GetSet()
        {
            var v = new VarInt64();
            v.Value = -99999999999L;
            Assert.AreEqual(-99999999999L, v.Value);
        }

        [Test]
        public void VarInt64_Clear_ResetsToDefault()
        {
            var v = new VarInt64();
            v.Value = 123456789L;
            v.Clear();
            Assert.AreEqual((long)0, v.Value);
        }

        [Test]
        public void VarInt64_Type_ReturnsInt64()
        {
            var v = new VarInt64();
            Assert.AreEqual(typeof(long), v.Type);
        }

        [Test]
        public void VarInt64_SetValue_WrongType_Throws()
        {
            var v = new VarInt64();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("bad"));
        }

        [Test]
        public void VarInt64_ImplicitFromLong()
        {
            VarInt64 v = 1234567890123L;
            Assert.AreEqual(1234567890123L, v.Value);
        }

        [Test]
        public void VarInt64_ImplicitToLong()
        {
            var v = new VarInt64();
            v.Value = 1234567890123L;
            long result = v;
            Assert.AreEqual(1234567890123L, result);
        }

        #endregion

        #region VarUInt64

        [Test]
        public void VarUInt64_DefaultValue_IsZero()
        {
            var v = new VarUInt64();
            Assert.AreEqual((ulong)0, v.Value);
        }

        [Test]
        public void VarUInt64_Value_GetSet()
        {
            var v = new VarUInt64();
            v.Value = 18446744073709551615UL;
            Assert.AreEqual(18446744073709551615UL, v.Value);
        }

        [Test]
        public void VarUInt64_Clear_ResetsToDefault()
        {
            var v = new VarUInt64();
            v.Value = 9999999999UL;
            v.Clear();
            Assert.AreEqual((ulong)0, v.Value);
        }

        [Test]
        public void VarUInt64_Type_ReturnsUInt64()
        {
            var v = new VarUInt64();
            Assert.AreEqual(typeof(ulong), v.Type);
        }

        [Test]
        public void VarUInt64_SetValue_WrongType_Throws()
        {
            var v = new VarUInt64();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(-1));
        }

        [Test]
        public void VarUInt64_ImplicitFromULong()
        {
            VarUInt64 v = 9999999999UL;
            Assert.AreEqual(9999999999UL, v.Value);
        }

        [Test]
        public void VarUInt64_ImplicitToULong()
        {
            var v = new VarUInt64();
            v.Value = 9999999999UL;
            ulong result = v;
            Assert.AreEqual(9999999999UL, result);
        }

        #endregion

        #region VarSingle

        [Test]
        public void VarSingle_DefaultValue_IsZero()
        {
            var v = new VarSingle();
            Assert.AreEqual(0f, v.Value);
        }

        [Test]
        public void VarSingle_Value_GetSet()
        {
            var v = new VarSingle();
            v.Value = 3.14f;
            Assert.AreEqual(3.14f, v.Value, 0.0001f);
        }

        [Test]
        public void VarSingle_Clear_ResetsToDefault()
        {
            var v = new VarSingle();
            v.Value = 99.9f;
            v.Clear();
            Assert.AreEqual(0f, v.Value);
        }

        [Test]
        public void VarSingle_Type_ReturnsSingle()
        {
            var v = new VarSingle();
            Assert.AreEqual(typeof(float), v.Type);
        }

        [Test]
        public void VarSingle_SetValue_WrongType_Throws()
        {
            var v = new VarSingle();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("not a float"));
        }

        [Test]
        public void VarSingle_ImplicitFromFloat()
        {
            VarSingle v = 2.5f;
            Assert.AreEqual(2.5f, v.Value);
        }

        [Test]
        public void VarSingle_ImplicitToFloat()
        {
            var v = new VarSingle();
            v.Value = 2.5f;
            float result = v;
            Assert.AreEqual(2.5f, result);
        }

        [Test]
        public void VarSingle_ReferencePool_AcquireRelease()
        {
            var v = ReferencePool.Acquire<VarSingle>();
            v.Value = 1.1f;
            ReferencePool.Release(v);
            var v2 = ReferencePool.Acquire<VarSingle>();
            Assert.AreEqual(0f, v2.Value);
        }

        #endregion

        #region VarDouble

        [Test]
        public void VarDouble_DefaultValue_IsZero()
        {
            var v = new VarDouble();
            Assert.AreEqual(0.0, v.Value);
        }

        [Test]
        public void VarDouble_Value_GetSet()
        {
            var v = new VarDouble();
            v.Value = 3.14159265358979;
            Assert.AreEqual(3.14159265358979, v.Value, 0.0000001);
        }

        [Test]
        public void VarDouble_Clear_ResetsToDefault()
        {
            var v = new VarDouble();
            v.Value = 99.9;
            v.Clear();
            Assert.AreEqual(0.0, v.Value);
        }

        [Test]
        public void VarDouble_Type_ReturnsDouble()
        {
            var v = new VarDouble();
            Assert.AreEqual(typeof(double), v.Type);
        }

        [Test]
        public void VarDouble_SetValue_WrongType_Throws()
        {
            var v = new VarDouble();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("bad"));
        }

        [Test]
        public void VarDouble_ImplicitFromDouble()
        {
            VarDouble v = 2.718;
            Assert.AreEqual(2.718, v.Value);
        }

        [Test]
        public void VarDouble_ImplicitToDouble()
        {
            var v = new VarDouble();
            v.Value = 2.718;
            double result = v;
            Assert.AreEqual(2.718, result);
        }

        #endregion

        #region VarDecimal

        [Test]
        public void VarDecimal_DefaultValue_IsZero()
        {
            var v = new VarDecimal();
            Assert.AreEqual(0m, v.Value);
        }

        [Test]
        public void VarDecimal_Value_GetSet()
        {
            var v = new VarDecimal();
            v.Value = 123.456m;
            Assert.AreEqual(123.456m, v.Value);
        }

        [Test]
        public void VarDecimal_Clear_ResetsToDefault()
        {
            var v = new VarDecimal();
            v.Value = 999.99m;
            v.Clear();
            Assert.AreEqual(0m, v.Value);
        }

        [Test]
        public void VarDecimal_Type_ReturnsDecimal()
        {
            var v = new VarDecimal();
            Assert.AreEqual(typeof(decimal), v.Type);
        }

        [Test]
        public void VarDecimal_SetValue_WrongType_Throws()
        {
            var v = new VarDecimal();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("bad"));
        }

        [Test]
        public void VarDecimal_ImplicitFromDecimal()
        {
            VarDecimal v = 456.789m;
            Assert.AreEqual(456.789m, v.Value);
        }

        [Test]
        public void VarDecimal_ImplicitToDecimal()
        {
            var v = new VarDecimal();
            v.Value = 456.789m;
            decimal result = v;
            Assert.AreEqual(456.789m, result);
        }

        #endregion

        #region VarString

        [Test]
        public void VarString_DefaultValue_IsNull()
        {
            var v = new VarString();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarString_Value_GetSet()
        {
            var v = new VarString();
            v.Value = "hello";
            Assert.AreEqual("hello", v.Value);
        }

        [Test]
        public void VarString_Clear_ResetsToNull()
        {
            var v = new VarString();
            v.Value = "hello";
            v.Clear();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarString_Type_ReturnsString()
        {
            var v = new VarString();
            Assert.AreEqual(typeof(string), v.Type);
        }

        [Test]
        public void VarString_SetValue_CorrectType()
        {
            var v = new VarString();
            v.SetValue("test");
            Assert.AreEqual("test", v.Value);
        }

        [Test]
        public void VarString_SetValue_Null_SetsNull()
        {
            var v = new VarString();
            v.Value = "hello";
            v.SetValue(null);
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarString_SetValue_WrongType_Throws()
        {
            var v = new VarString();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(123));
        }

        [Test]
        public void VarString_ToString_WithValue()
        {
            var v = new VarString();
            v.Value = "test";
            Assert.AreEqual("test", v.ToString());
        }

        [Test]
        public void VarString_ToString_WithNull_ReturnsNullTag()
        {
            var v = new VarString();
            Assert.AreEqual("<Null>", v.ToString());
        }

        [Test]
        public void VarString_ImplicitFromString()
        {
            VarString v = "hello";
            Assert.AreEqual("hello", v.Value);
        }

        [Test]
        public void VarString_ImplicitToString()
        {
            var v = new VarString();
            v.Value = "hello";
            string result = v;
            Assert.AreEqual("hello", result);
        }

        [Test]
        public void VarString_ReferencePool_AcquireRelease()
        {
            var v = ReferencePool.Acquire<VarString>();
            v.Value = "test";
            ReferencePool.Release(v);
            var v2 = ReferencePool.Acquire<VarString>();
            Assert.IsNull(v2.Value);
        }

        #endregion

        #region VarDateTime

        [Test]
        public void VarDateTime_DefaultValue_IsDefaultDateTime()
        {
            var v = new VarDateTime();
            Assert.AreEqual(default(DateTime), v.Value);
        }

        [Test]
        public void VarDateTime_Value_GetSet()
        {
            var v = new VarDateTime();
            var dt = new DateTime(2025, 6, 15, 12, 30, 0);
            v.Value = dt;
            Assert.AreEqual(dt, v.Value);
        }

        [Test]
        public void VarDateTime_Clear_ResetsToDefault()
        {
            var v = new VarDateTime();
            v.Value = DateTime.Now;
            v.Clear();
            Assert.AreEqual(default(DateTime), v.Value);
        }

        [Test]
        public void VarDateTime_Type_ReturnsDateTime()
        {
            var v = new VarDateTime();
            Assert.AreEqual(typeof(DateTime), v.Type);
        }

        [Test]
        public void VarDateTime_SetValue_WrongType_Throws()
        {
            var v = new VarDateTime();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("bad"));
        }

        [Test]
        public void VarDateTime_ToString_ReturnsStringRepresentation()
        {
            var v = new VarDateTime();
            var dt = new DateTime(2025, 1, 1);
            v.Value = dt;
            Assert.AreEqual(dt.ToString(), v.ToString());
        }

        [Test]
        public void VarDateTime_ImplicitFromDateTime()
        {
            var dt = new DateTime(2025, 6, 15);
            VarDateTime v = dt;
            Assert.AreEqual(dt, v.Value);
        }

        [Test]
        public void VarDateTime_ImplicitToDateTime()
        {
            var v = new VarDateTime();
            var dt = new DateTime(2025, 6, 15);
            v.Value = dt;
            DateTime result = v;
            Assert.AreEqual(dt, result);
        }

        #endregion

        #region VarByteArray

        [Test]
        public void VarByteArray_DefaultValue_IsNull()
        {
            var v = new VarByteArray();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarByteArray_Value_GetSet()
        {
            var v = new VarByteArray();
            var data = new byte[] { 1, 2, 3 };
            v.Value = data;
            Assert.AreEqual(data, v.Value);
        }

        [Test]
        public void VarByteArray_Clear_ResetsToNull()
        {
            var v = new VarByteArray();
            v.Value = new byte[] { 1, 2, 3 };
            v.Clear();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarByteArray_Type_ReturnsByteArray()
        {
            var v = new VarByteArray();
            Assert.AreEqual(typeof(byte[]), v.Type);
        }

        [Test]
        public void VarByteArray_SetValue_WrongType_Throws()
        {
            var v = new VarByteArray();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("bad"));
        }

        [Test]
        public void VarByteArray_ToString_WithValue()
        {
            var v = new VarByteArray();
            v.Value = new byte[] { 1, 2, 3 };
            Assert.AreEqual(v.Value.ToString(), v.ToString());
        }

        [Test]
        public void VarByteArray_ToString_WithNull_ReturnsNullTag()
        {
            var v = new VarByteArray();
            Assert.AreEqual("<Null>", v.ToString());
        }

        [Test]
        public void VarByteArray_ImplicitFromByteArray()
        {
            var data = new byte[] { 10, 20, 30 };
            VarByteArray v = data;
            Assert.AreEqual(data, v.Value);
        }

        [Test]
        public void VarByteArray_ImplicitToByteArray()
        {
            var v = new VarByteArray();
            var data = new byte[] { 10, 20, 30 };
            v.Value = data;
            byte[] result = v;
            Assert.AreEqual(data, result);
        }

        #endregion

        #region VarCharArray

        [Test]
        public void VarCharArray_DefaultValue_IsNull()
        {
            var v = new VarCharArray();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarCharArray_Value_GetSet()
        {
            var v = new VarCharArray();
            var data = new char[] { 'a', 'b', 'c' };
            v.Value = data;
            Assert.AreEqual(data, v.Value);
        }

        [Test]
        public void VarCharArray_Clear_ResetsToNull()
        {
            var v = new VarCharArray();
            v.Value = new char[] { 'a', 'b', 'c' };
            v.Clear();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarCharArray_Type_ReturnsCharArray()
        {
            var v = new VarCharArray();
            Assert.AreEqual(typeof(char[]), v.Type);
        }

        [Test]
        public void VarCharArray_SetValue_WrongType_Throws()
        {
            var v = new VarCharArray();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(123));
        }

        [Test]
        public void VarCharArray_ToString_WithNull_ReturnsNullTag()
        {
            var v = new VarCharArray();
            Assert.AreEqual("<Null>", v.ToString());
        }

        [Test]
        public void VarCharArray_ImplicitFromCharArray()
        {
            var data = new char[] { 'x', 'y' };
            VarCharArray v = data;
            Assert.AreEqual(data, v.Value);
        }

        [Test]
        public void VarCharArray_ImplicitToCharArray()
        {
            var v = new VarCharArray();
            var data = new char[] { 'x', 'y' };
            v.Value = data;
            char[] result = v;
            Assert.AreEqual(data, result);
        }

        #endregion

        #region VarObject

        [Test]
        public void VarObject_DefaultValue_IsNull()
        {
            var v = new VarObject();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarObject_Value_GetSet_WithObject()
        {
            var v = new VarObject();
            var obj = new object();
            v.Value = obj;
            Assert.AreSame(obj, v.Value);
        }

        [Test]
        public void VarObject_Clear_ResetsToNull()
        {
            var v = new VarObject();
            v.Value = new object();
            v.Clear();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarObject_Type_ReturnsObject()
        {
            var v = new VarObject();
            Assert.AreEqual(typeof(object), v.Type);
        }

        [Test]
        public void VarObject_SetValue_Null_SetsNull()
        {
            var v = new VarObject();
            v.Value = new object();
            v.SetValue(null);
            Assert.IsNull(v.Value);
        }

        [Test]
        public void VarObject_SetValue_BoxedInt_WorksBecauseBoxedIntIsObject()
        {
            var v = new VarObject();
            v.SetValue(42);
            Assert.AreEqual(42, v.Value);
        }

        [Test]
        public void VarObject_ToString_WithNull_ReturnsNullTag()
        {
            var v = new VarObject();
            Assert.AreEqual("<Null>", v.ToString());
        }

        [Test]
        public void VarObject_ToString_WithValue()
        {
            var v = new VarObject();
            v.Value = 42;
            Assert.AreEqual("42", v.ToString());
        }

        [Test]
        public void VarObject_ReferencePool_AcquireRelease()
        {
            var v = ReferencePool.Acquire<VarObject>();
            v.Value = new object();
            ReferencePool.Release(v);
            var v2 = ReferencePool.Acquire<VarObject>();
            Assert.IsNull(v2.Value);
        }

        #endregion

        #region Variable<T> Base Class

        [Test]
        public void Variable_Generic_SetValue_NullForValueType_Throws()
        {
            var v = new VarInt32();
            v.SetValue(null);
            Assert.AreEqual(0, v.Value);
        }

        [Test]
        public void Variable_Generic_GetValue_BoxedCorrectly()
        {
            var v = new VarInt32();
            v.Value = 42;
            object boxed = v.GetValue();
            Assert.IsInstanceOf<int>(boxed);
            Assert.AreEqual(42, boxed);
        }

        [Test]
        public void Variable_Generic_SetValue_BoxedCorrectType()
        {
            var v = new VarInt32();
            v.SetValue((object)99);
            Assert.AreEqual(99, v.Value);
        }

        [Test]
        public void Variable_Generic_Type_ReturnsUnderlyingType()
        {
            Variable v = new VarBoolean();
            Assert.AreEqual(typeof(bool), v.Type);
        }

        [Test]
        public void Variable_Generic_Clear_CalledOnBaseVariable()
        {
            Variable v = new VarInt32();
            v.SetValue(42);
            v.Clear();
            Assert.AreEqual(0, ((VarInt32)v).Value);
        }

        #endregion

        #region Cross-Type SetValue Safety

        [Test]
        public void VarInt32_SetValue_WithLong_Throws()
        {
            var v = new VarInt32();
            Assert.Throws<GameFrameworkException>(() => v.SetValue((long)42));
        }

        [Test]
        public void VarSingle_SetValue_WithDouble_Throws()
        {
            var v = new VarSingle();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(3.14));
        }

        [Test]
        public void VarString_SetValue_WithInt_Throws()
        {
            var v = new VarString();
            Assert.Throws<GameFrameworkException>(() => v.SetValue(42));
        }

        #endregion
    }
}
