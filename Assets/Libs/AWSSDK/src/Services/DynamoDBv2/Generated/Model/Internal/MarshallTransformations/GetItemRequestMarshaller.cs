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
    /// GetItem Request Marshaller
    /// </summary>       
    public class GetItemRequestMarshaller : IMarshaller<IRequest, GetItemRequest> , IMarshaller<IRequest,AmazonWebServiceRequest>
    {
        public IRequest Marshall(AmazonWebServiceRequest input)
        {
            return this.Marshall((GetItemRequest)input);
        }

        public IRequest Marshall(GetItemRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.DynamoDBv2");
            string target = "DynamoDB_20120810.GetItem";
            request.Headers["X-Amz-Target"] = target;
            request.Headers["Content-Type"] = "application/x-amz-json-1.0";
            request.HttpMethod = "POST";

            string uriResourcePath = "/";
            request.ResourcePath = uriResourcePath;
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                var context = new JsonMarshallerContext(request, writer);
                if(publicRequest.IsSetAttributesToGet())
                {
                    context.Writer.WritePropertyName("AttributesToGet");
                    context.Writer.WriteArrayStart();
                    foreach(var publicRequestAttributesToGetListValue in publicRequest.AttributesToGet)
                    {
                            context.Writer.Write(publicRequestAttributesToGetListValue);
                    }
                    context.Writer.WriteArrayEnd();
                }

                if(publicRequest.IsSetConsistentRead())
                {
                    context.Writer.WritePropertyName("ConsistentRead");
                    context.Writer.Write(publicRequest.ConsistentRead);
                }

                if(publicRequest.IsSetExpressionAttributeNames())
                {
                    context.Writer.WritePropertyName("ExpressionAttributeNames");
                    context.Writer.WriteObjectStart();
                    foreach (var publicRequestExpressionAttributeNamesKvp in publicRequest.ExpressionAttributeNames)
                    {
                        context.Writer.WritePropertyName(publicRequestExpressionAttributeNamesKvp.Key);
                        var publicRequestExpressionAttributeNamesValue = publicRequestExpressionAttributeNamesKvp.Value;

                            context.Writer.Write(publicRequestExpressionAttributeNamesValue);
                    }
                    context.Writer.WriteObjectEnd();
                }

                if(publicRequest.IsSetKey())
                {
                    context.Writer.WritePropertyName("Key");
                    context.Writer.WriteObjectStart();
                    foreach (var publicRequestKeyKvp in publicRequest.Key)
                    {
                        context.Writer.WritePropertyName(publicRequestKeyKvp.Key);
                        var publicRequestKeyValue = publicRequestKeyKvp.Value;

                        context.Writer.WriteObjectStart();

                        var marshaller = AttributeValueMarshaller.Instance;
                        marshaller.Marshall(publicRequestKeyValue, context);

                        context.Writer.WriteObjectEnd();
                    }
                    context.Writer.WriteObjectEnd();
                }

                if(publicRequest.IsSetProjectionExpression())
                {
                    context.Writer.WritePropertyName("ProjectionExpression");
                    context.Writer.Write(publicRequest.ProjectionExpression);
                }

                if(publicRequest.IsSetReturnConsumedCapacity())
                {
                    context.Writer.WritePropertyName("ReturnConsumedCapacity");
                    context.Writer.Write(publicRequest.ReturnConsumedCapacity);
                }

                if(publicRequest.IsSetTableName())
                {
                    context.Writer.WritePropertyName("TableName");
                    context.Writer.Write(publicRequest.TableName);
                }

        
                writer.WriteObjectEnd();
                string snippet = stringWriter.ToString();
                request.Content = System.Text.Encoding.UTF8.GetBytes(snippet);
            }


            return request;
        }


    }
}
