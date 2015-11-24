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

/*
 * Do not modify this file. This file is generated from the dynamodb-2012-08-10.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.DynamoDBv2.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for KeysAndAttributes Object
    /// </summary>  
    public class KeysAndAttributesUnmarshaller : IUnmarshaller<KeysAndAttributes, XmlUnmarshallerContext>, IUnmarshaller<KeysAndAttributes, JsonUnmarshallerContext>
    {
        KeysAndAttributes IUnmarshaller<KeysAndAttributes, XmlUnmarshallerContext>.Unmarshall(XmlUnmarshallerContext context)
        {
            throw new NotImplementedException();
        }

        public KeysAndAttributes Unmarshall(JsonUnmarshallerContext context)
        {
            context.Read();
            if (context.CurrentTokenType == JsonToken.Null) 
                return null;

            KeysAndAttributes unmarshalledObject = new KeysAndAttributes();
        
            int targetDepth = context.CurrentDepth;
            while (context.ReadAtDepth(targetDepth))
            {
                if (context.TestExpression("AttributesToGet", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<string, StringUnmarshaller>(StringUnmarshaller.Instance);
                    unmarshalledObject.AttributesToGet = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("ConsistentRead", targetDepth))
                {
                    var unmarshaller = BoolUnmarshaller.Instance;
                    unmarshalledObject.ConsistentRead = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("ExpressionAttributeNames", targetDepth))
                {
                    var unmarshaller = new DictionaryUnmarshaller<string, string, StringUnmarshaller, StringUnmarshaller>(StringUnmarshaller.Instance, StringUnmarshaller.Instance);
                    unmarshalledObject.ExpressionAttributeNames = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("Keys", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<Dictionary<string, AttributeValue>, DictionaryUnmarshaller<string, AttributeValue, StringUnmarshaller, AttributeValueUnmarshaller>>(new DictionaryUnmarshaller<string, AttributeValue, StringUnmarshaller, AttributeValueUnmarshaller>(StringUnmarshaller.Instance, AttributeValueUnmarshaller.Instance));
                    unmarshalledObject.Keys = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("ProjectionExpression", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    unmarshalledObject.ProjectionExpression = unmarshaller.Unmarshall(context);
                    continue;
                }
            }
          
            return unmarshalledObject;
        }


        private static KeysAndAttributesUnmarshaller _instance = new KeysAndAttributesUnmarshaller();        

        public static KeysAndAttributesUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
