﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Voucherify.Core.Tests.Serialization.TestModel;
using Voucherify.Core.Serialization;

namespace Voucherify.Core.Tests.Serialization
{
    [TestClass]
    public class JsonSerializerTest
    {
        [TestMethod]
        public void JsonSerializerSerialize()
        {
            //-- Arrange
            JsonType jsonObject = new JsonType()
            {
                Property = "test_property_value",
                Array = new string[] { "test_array_value_1", "test_array_value_2" },
                Dictionary = new Dictionary<string, object>()
                {
                    { "property_1", "value_1" },
                    { "property_2", "value_2" },
                    { "property_3", new JsonType() { Property = "property_nested" } }
                },
                Enum = "Enum-Value-2"
            };
            string jsonSerializedExpectedObject = "{\"property_test\":\"test_property_value\",\"array_test\":[\"test_array_value_1\",\"test_array_value_2\"],\"dictionary_test\":{\"property_1\":\"value_1\",\"property_2\":\"value_2\",\"property_3\":{\"property_test\":\"property_nested\",\"array_test\":null,\"dictionary_test\":null,\"enum_test\":null}},\"enum_test\":\"Enum-Value-2\"}";

            //-- Act
            string jsonSerializedObject = new JsonSerializer<JsonType>(new List<JsonConverter>() { }).Serialize(jsonObject);

            //-- Assert
            Assert.AreEqual(jsonSerializedExpectedObject, jsonSerializedObject);
        }

        [TestMethod]
        public void JsonSerializerDeserialize()
        {
            //-- Arrange
            JsonType jsonExpectedDeserializedObject = new JsonType()
            {
                Property = "test_property_value",
                Array = new string[] { "test_array_value_1", "test_array_value_2" },
                Dictionary = new Dictionary<string, object>()
                {
                    { "property_1", "value_1" },
                    { "property_2", "value_2" },
                    { "property_3", new JsonType() { Property = "property_nested" } }
                },
                Enum = "Enum-Value-2"
            };
            string jsonObject = "{\"property_test\":\"test_property_value\",\"array_test\":[\"test_array_value_1\",\"test_array_value_2\"],\"dictionary_test\":{\"property_1\":\"value_1\",\"property_2\":\"value_2\",\"property_3\":{\"property_test\":\"property_nested\",\"array_test\":null,\"dictionary_test\":null,\"enum_test\":null}},\"enum_test\":\"Enum-Value-2\"}";

            //-- Act
            JsonType jsonSerializedObject = new JsonSerializer<JsonType>(new List<JsonConverter>() { }).Deserialize(jsonObject);

            //-- Assert
            Assert.AreEqual(jsonExpectedDeserializedObject.Property, jsonSerializedObject.Property);
            Assert.AreEqual(jsonExpectedDeserializedObject.Enum, jsonSerializedObject.Enum);
            Assert.AreEqual(jsonExpectedDeserializedObject.Array.Count, jsonSerializedObject.Array.Count);
            Assert.AreEqual(jsonExpectedDeserializedObject.Array[0], jsonSerializedObject.Array[0]);
            Assert.AreEqual(jsonExpectedDeserializedObject.Array[1], jsonSerializedObject.Array[1]);
            Assert.AreEqual(jsonExpectedDeserializedObject.Dictionary.Count, jsonSerializedObject.Dictionary.Count);
            Assert.AreEqual(jsonExpectedDeserializedObject.Dictionary["property_1"], jsonSerializedObject.Dictionary["property_1"]);
            Assert.AreEqual(jsonExpectedDeserializedObject.Dictionary["property_2"], jsonSerializedObject.Dictionary["property_2"]);
            Assert.AreEqual(jsonExpectedDeserializedObject.Dictionary["property_3"] is object, jsonSerializedObject.Dictionary["property_1"] is object);
        }
    }
}