//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the Amazon Software License (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located at
// 
//     http://aws.amazon.com/asl/
// 
// or in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// A collection of attribute key-value pairs that defines
    /// an item in DynamoDB.
    /// </summary>
    public class Document : DynamoDBEntry, IDictionary<string, DynamoDBEntry>
    {
        #region Private/internal members

        private Dictionary<string, DynamoDBEntry> originalValues;
        private Dictionary<string, DynamoDBEntry> currentValues;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an empty Document.
        /// </summary>
        public Document()
        {
            originalValues = new Dictionary<string, DynamoDBEntry>();
            currentValues = new Dictionary<string, DynamoDBEntry>();
        }

        /// <summary>
        /// Constructs a Document with the passed-in values as its attribute values.
        /// </summary>
        /// <param name="values"></param>
        public Document(Dictionary<string, DynamoDBEntry> values)
        {
            originalValues = new Dictionary<string, DynamoDBEntry>();
            currentValues = new Dictionary<string, DynamoDBEntry>(values);
        }

        internal Document(Document source)
        {
            originalValues = new Dictionary<string, DynamoDBEntry>(source.originalValues);
            currentValues = new Dictionary<string, DynamoDBEntry>(source.currentValues);
        }

        #endregion

        #region Properties/accessors

        /// <summary>
        /// Attribute accessor, allows getting or setting of an individual attribute.
        /// </summary>
        /// <param name="key">Name of the attribute.</param>
        /// <returns>Current value of the attribute.</returns>
        public DynamoDBEntry this[string key]
        {
            get
            {
                return currentValues[key];
            }
            set
            {
                if (value == null)
                {
                    currentValues[key] = new Primitive();
                }
                else
                {
                    currentValues[key] = value;
                }
            }
        }

        /// <summary>
        /// Returns true if the attribute has been changed.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>True if the attribute has been changed.</returns>
        public bool IsAttributeChanged(string attributeName)
        {
            DynamoDBEntry original;
            this.originalValues.TryGetValue(attributeName, out original);           

            DynamoDBEntry current;
            this.currentValues.TryGetValue(attributeName, out current);

            if ((original != null && current == null) || (original == null && current != null))
                return true;

            if (original == null && current == null)
                return false;

            return !original.Equals(current);
        }

        /// <summary>
        /// Returns true if the document contains attributes that have not been saved.
        /// </summary>
        /// <returns>True if the document contains attributes that have not been saved.</returns>
        public bool IsDirty()
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            foreach (string key in currentValues.Keys)
            {
                keys.Add(key, key);
            }

            foreach (string key in originalValues.Keys)
            {
                if (!keys.ContainsKey(key))
                {
                    keys.Add(key, key);
                }
            }

            foreach (string key in keys.Keys)
            {
                if (this.IsAttributeChanged(key))
                {
                    return true;
                }
            }

            return false;
        }

        internal void CommitChanges()
        {
            this.originalValues.Clear();
            foreach (var kvp in currentValues)
            {
                this.originalValues[kvp.Key] = kvp.Value.Clone() as DynamoDBEntry;
            }
        }

        /// <summary>
        /// Gets the value associated with the specified attribute value.
        /// </summary>
        /// <param name="attributeName">Attribute name</param>
        /// <param name="entry">
        /// If the specified attribute value is found, returns the value associated with the 
        /// attribute; otherwise, null.
        /// </param>
        /// <returns>True if attribute is found; false otherwise</returns>
        public bool TryGetValue(string attributeName, out DynamoDBEntry entry)
        {
            return this.currentValues.TryGetValue(attributeName, out entry);
        }

        /// <summary>
        /// Determines if a specific attribute is set on the Document.
        /// </summary>
        /// <param name="attributeName">Attribute name</param>
        /// <returns>Returns true if the specified attribute is found; false otherwise.</returns>
        public bool Contains(string attributeName)
        {
            return this.currentValues.ContainsKey(attributeName);
        }

        /// <summary>
        /// Returns a new instance of Document where all unconverted .NET types
        /// are converted to DynamoDBEntry types using a specific conversion.
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public Document ForceConversion(DynamoDBEntryConversion conversion)
        {
            Document newDocument = new Document();
            foreach(var kvp in this)
            {
                string name = kvp.Key;
                DynamoDBEntry entry = kvp.Value;

                var unconvertedEntry = entry as UnconvertedDynamoDBEntry;
                if (unconvertedEntry != null)
                    entry = unconvertedEntry.Convert(conversion);

                var doc = entry as Document;
                if (doc != null)
                    entry = doc.ForceConversion(conversion);

                var list = entry as DynamoDBList;
                if (list != null)
                    entry = list.ForceConversion(conversion);

                newDocument[name] = entry;
            }

            return newDocument;
        }

        #endregion

        #region DynamoDB conversion

        /// <summary>
        /// <para>
        /// Converts the current Document into a matching JSON string.
        /// </para>
        /// <para>
        /// DynamoDB types are a superset of JSON types, thus the following DynamoDB cannot
        /// be properly represented as JSON data:
        ///  PrimitiveList (SS, NS, BS types) - these sets will be converted to JSON arrays
        ///  Binary Primitive (B type) - binary data will be converted to Base64 strings
        /// </para>
        /// <para>
        /// If the resultant JSON is passed to Document.FromJson, the binary values will be
        /// treated as Base64 strings. Invoke Document.DecodeBase64Attributes to decode these
        /// strings into binary data.
        /// </para>
        /// </summary>
        /// <returns>JSON string corresponding to the current Document.</returns>
        public string ToJson()
        {
            return JsonUtils.ToJson(this, prettyPrint: false);
        }

        /// <summary>
        /// <para>
        /// Converts the current Document into a matching pretty JSON string.
        /// </para>
        /// <para>
        /// DynamoDB types are a superset of JSON types, thus the following DynamoDB cannot
        /// be properly represented as JSON data:
        ///  PrimitiveList (SS, NS, BS types) - these sets will be converted to JSON arrays
        ///  Binary Primitive (B type) - binary data will be converted to Base64 strings
        /// </para>
        /// <para>
        /// If the resultant JSON is passed to Document.FromJson, the binary values will be
        /// treated as Base64 strings. Invoke Document.DecodeBase64Attributes to decode these
        /// strings into binary data.
        /// </para>
        /// </summary>
        /// <returns>JSON string corresponding to the current Document.</returns>
        public string ToJsonPretty()
        {
            return JsonUtils.ToJson(this, prettyPrint: true);
        }

        /// <summary>
        /// Creates a map of attribute names mapped to AttributeValue objects.
        /// Converts .NET types using the conversion specified by AWSConfigs.DynamoDBConfig.ConversionSchema
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, AttributeValue> ToAttributeMap()
        {
            return ToAttributeMap(DynamoDBEntryConversion.CurrentConversion);
        }

        /// <summary>
        /// Creates a map of attribute names mapped to AttributeValue objects.
        /// </summary>
        /// <param name="conversion">Conversion to use for converting .NET values to DynamoDB values.</param>
        /// <returns></returns>
        public Dictionary<string, AttributeValue> ToAttributeMap(DynamoDBEntryConversion conversion)
        {
            if (conversion == null) throw new ArgumentNullException("conversion");

            Dictionary<string, AttributeValue> ret = new Dictionary<string, AttributeValue>();

            foreach (var attribute in currentValues)
            {
                AttributeValue value = attribute.Value.ConvertToAttributeValue(new AttributeConversionConfig(conversion));
                if (value != null)
                {
                    ret.Add(attribute.Key, value);
                }
            }
            
            return ret;
        }

        /// <summary>
        /// Creates a map of attribute names mapped to ExpectedAttributeValue objects.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ExpectedAttributeValue> ToExpectedAttributeMap()
        {
            return ToExpectedAttributeMap(DynamoDBEntryConversion.CurrentConversion);
        }
        /// <summary>
        /// Creates a map of attribute names mapped to ExpectedAttributeValue objects.
        /// </summary>
        /// <param name="conversion">Conversion to use for converting .NET values to DynamoDB values.</param>
        /// <returns></returns>
        public Dictionary<string, ExpectedAttributeValue> ToExpectedAttributeMap(DynamoDBEntryConversion conversion)
        {
            if (conversion == null) throw new ArgumentNullException("conversion");
            Dictionary<string, ExpectedAttributeValue> ret = new Dictionary<string, ExpectedAttributeValue>();

            foreach (var attribute in currentValues)
            {
                ret.Add(attribute.Key, attribute.Value.ConvertToExpectedAttributeValue(new AttributeConversionConfig(conversion)));
            }

            return ret;
        }

        /// <summary>
        /// Creates a map of attribute names mapped to AttributeValueUpdate objects.
        /// </summary>
        /// <param name="changedAttributesOnly">If true, only attributes that have been changed will be in the map.</param>
        /// <returns></returns>
        public Dictionary<string, AttributeValueUpdate> ToAttributeUpdateMap(bool changedAttributesOnly)
        {
            return ToAttributeUpdateMap(DynamoDBEntryConversion.CurrentConversion, changedAttributesOnly);
        }

        /// <summary>
        /// Creates a map of attribute names mapped to AttributeValueUpdate objects.
        /// </summary>
        /// <param name="changedAttributesOnly">If true, only attributes that have been changed will be in the map.</param>
        /// <param name="conversion">Conversion to use for converting .NET values to DynamoDB values.</param>
        /// <returns></returns>
        public Dictionary<string, AttributeValueUpdate> ToAttributeUpdateMap(DynamoDBEntryConversion conversion, bool changedAttributesOnly)
        {
            if (conversion == null) throw new ArgumentNullException("conversion");
            Dictionary<string, AttributeValueUpdate> ret = new Dictionary<string, AttributeValueUpdate>();

            foreach (var attribute in currentValues)
            {
                string name = attribute.Key;
                DynamoDBEntry value = attribute.Value;

                if (!changedAttributesOnly || this.IsAttributeChanged(name))
                {
                    ret.Add(name, value.ConvertToAttributeUpdateValue(new AttributeConversionConfig(conversion)));
                }
            }

            return ret;
        }

        /// <summary>
        /// Returns the names of all the attributes.
        /// </summary>
        /// <returns>List of attribute names.</returns>
        public List<String> GetAttributeNames()
        {
            return new List<string>(this.currentValues.Keys);
        }

        /// <summary>
        /// <para>
        /// Decodes root-level Base64-encoded strings to their binary representations.
        /// Use this method if the Document was constructed from JSON that contains
        /// base64-encoded binary values, which result from calling ToJson on a Document
        /// with binary data.
        /// </para>
        /// <para>
        /// Individual strings become binary data.
        /// List and sets of Base64-encoded strings become lists and sets of binary data.
        /// </para>
        /// </summary>
        /// <param name="attributeNames">Names of root-level attributes to decode.</param>
        public void DecodeBase64Attributes(params string[] attributeNames)
        {
            JsonUtils.DecodeBase64Attributes(this, attributeNames);
        }

        #endregion

        #region Attribute to DynamoDBEntry conversion

        internal static DynamoDBEntry AttributeValueToDynamoDBEntry(AttributeValue attributeValue)
        {
            Primitive primitive;
            if (TryToPrimitive(attributeValue, out primitive))
                return primitive;

            PrimitiveList primitiveList;
            if (TryToPrimitiveList(attributeValue, out primitiveList))
                return primitiveList;

            DynamoDBBool ddbBool;
            if (TryToDynamoDBBool(attributeValue, out ddbBool))
                return ddbBool;

            DynamoDBNull ddbNull;
            if (TryToDynamoDBNull(attributeValue, out ddbNull))
                return ddbNull;

            DynamoDBList ddbList;
            if (TryToDynamoDBList(attributeValue, out ddbList))
                return ddbList;

            Document document;
            if (TryToDocument(attributeValue, out document))
                return document;

            return null;
        }

        private static bool TryToPrimitiveList(AttributeValue attributeValue, out PrimitiveList primitiveList)
        {
            primitiveList = null;
            Primitive primitive;

            if (attributeValue.IsSetSS())
            {
                primitiveList = new PrimitiveList(DynamoDBEntryType.String);
                foreach (string item in attributeValue.SS)
                {
                    primitive = new Primitive(item);
                    primitiveList.Add(primitive);
                }
            }
            else if (attributeValue.IsSetNS())
            {
                primitiveList = new PrimitiveList(DynamoDBEntryType.Numeric);
                foreach (string item in attributeValue.NS)
                {
                    primitive = new Primitive(item, true);
                    primitiveList.Add(primitive);
                }
            }
            else if (attributeValue.IsSetBS())
            {
                primitiveList = new PrimitiveList(DynamoDBEntryType.Binary);
                foreach (MemoryStream item in attributeValue.BS)
                {
                    primitive = new Primitive(item);
                    primitiveList.Add(primitive);
                }
            }

            return (primitiveList != null);
        }

        private static bool TryToPrimitive(AttributeValue attributeValue, out Primitive primitive)
        {
            primitive = null;
            if (attributeValue.IsSetS())
            {
                primitive = new Primitive(attributeValue.S);
            }
            else if (attributeValue.IsSetN())
            {
                primitive = new Primitive(attributeValue.N, true);
            }
            else if (attributeValue.IsSetB())
            {
                primitive = new Primitive(attributeValue.B);
            }

            return (primitive != null);
        }

        private static bool TryToDynamoDBBool(AttributeValue attributeValue, out DynamoDBBool ddbBool)
        {
            ddbBool = null;
            if (attributeValue.IsSetBOOL())
            {
                ddbBool = new DynamoDBBool(attributeValue.BOOL);
            }
            return (ddbBool != null);
        }

        private static bool TryToDynamoDBNull(AttributeValue attributeValue, out DynamoDBNull ddbNull)
        {
            ddbNull = null;
            if (attributeValue.IsSetNULL())
            {
                ddbNull = new DynamoDBNull();
            }
            return (ddbNull != null);
        }

        private static bool TryToDynamoDBList(AttributeValue attributeValue, out DynamoDBList list)
        {
            list = null;

            if (attributeValue.IsSetL())
            {
                var items = attributeValue.L;
                var entries = items.Select<AttributeValue,DynamoDBEntry>(AttributeValueToDynamoDBEntry);
                list = new DynamoDBList(entries);
            }

            return (list != null);
        }

        private static bool TryToDocument(AttributeValue attributeValue, out Document document)
        {
            document = null;
            
            if (attributeValue.IsSetM())
            {
                document = new Document();

                var items = attributeValue.M;
                foreach(var kvp in items)
                {
                    var name = kvp.Key;
                    var value = kvp.Value;

                    var entry = AttributeValueToDynamoDBEntry(value);
                    document[name] = entry;
                }
            }

            return (document != null);
        }

        #endregion

        #region Static methods
        
        /// <summary>
        /// Creates a Document from an attribute map.
        /// </summary>
        /// <param name="data">Map of attribute names to attribute values.</param>
        /// <returns>Document representing the data.</returns>
        public static Document FromAttributeMap(Dictionary<string, AttributeValue> data)
        {
            Document doc = new Document();

            if (data != null)
            {
                // Add Primitives and PrimitiveLists
                foreach (var attribute in data)
                {
                    string wholeKey = attribute.Key;
                    AttributeValue value = attribute.Value;

                    DynamoDBEntry convertedValue = AttributeValueToDynamoDBEntry(value);
                    if (convertedValue != null)
                        doc.currentValues[wholeKey] = convertedValue;
                }
            }

            doc.CommitChanges();
            return doc;
        }

        /// <summary>
        /// Creates a document from a JSON string.
        /// The conversion is as follows:
        ///  Objects are converted to DynamoDB M types.
        ///  Arrays are converted to DynamoDB L types.
        ///  Boolean types are converted to DynamoDB BOOL types.
        ///  Null values are converted to DynamoDB NULL types.
        ///  Numerics are converted to DynamoDB N types.
        ///  Strings are converted to DynamoDB S types.
        /// </summary>
        /// <param name="json">JSON string.</param>
        /// <returns>Document representing the JSON data.</returns>
        public static Document FromJson(string json)
        {
            return JsonUtils.FromJson(json);
        }
        
        #endregion

        #region IDictionary<string,DynamoDBEntry> Members

        public void Add(string key, DynamoDBEntry value)
        {
            currentValues.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return currentValues.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return currentValues.Keys; }
        }

        public bool Remove(string key)
        {
            return currentValues.Remove(key);
        }

        public ICollection<DynamoDBEntry> Values
        {
            get { return currentValues.Values; }
        }

        #endregion

        #region ICollection<KeyValuePair<string,DynamoDBEntry>> Members

        public void Add(KeyValuePair<string, DynamoDBEntry> item)
        {
            currentValues.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            currentValues.Clear();
        }

        public bool Contains(KeyValuePair<string, DynamoDBEntry> item)
        {
            //DynamoDBEntry value;
            //if (!this.TryGetValue(item.Key, out value))
            //    return false;
            //return value.Equals(item.Value);
            var icollection = (ICollection<KeyValuePair<string, DynamoDBEntry>>)currentValues;
            return icollection.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, DynamoDBEntry>[] array, int arrayIndex)
        {
            var icollection = (ICollection<KeyValuePair<string, DynamoDBEntry>>)currentValues;
            icollection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return currentValues.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, DynamoDBEntry> item)
        {
            var icollection = (ICollection<KeyValuePair<string, DynamoDBEntry>>)currentValues;
            return icollection.Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,DynamoDBEntry>> Members

        public IEnumerator<KeyValuePair<string, DynamoDBEntry>> GetEnumerator()
        {
            return currentValues.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return currentValues.GetEnumerator();
        }

        #endregion

        #region DynamoDBEntry overrides

        internal override AttributeValue ConvertToAttributeValue(AttributeConversionConfig conversionConfig)
        {
            var map = new Dictionary<string, AttributeValue>(StringComparer.Ordinal);
            foreach(var item in currentValues)
            {
                var key = item.Key;
                var entry = item.Value;
                AttributeValue entryAttributeValue;
                using (conversionConfig.CRT.Track(entry))
                {
                    entryAttributeValue = entry.ConvertToAttributeValue(conversionConfig);
                }

                if (entryAttributeValue != null)
                {
                    map[key] = entryAttributeValue;
                }
            }

            var attributeValue = new AttributeValue();
            attributeValue.M = map;
            attributeValue.IsMSet = true;

            return attributeValue;
        }

        public override object Clone()
        {
            var doc = new Document(this);
            return doc;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            var otherDocument = obj as Document;
            if (otherDocument == null)
                return false;

            if (Keys.Count != otherDocument.Keys.Count)
                return false;

            foreach(var key in Keys)
            {
                if (!otherDocument.ContainsKey(key))
                    return false;

                var a = this[key];
                var b = otherDocument[key];

                if (!a.Equals(b))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            foreach(var kvp in this)
            {
                string key = kvp.Key;
                DynamoDBEntry entry = kvp.Value;

                hashCode = Hashing.CombineHashes(hashCode, key.GetHashCode(), entry.GetHashCode());
            }

            return hashCode;
        }

        #endregion
    }
}
