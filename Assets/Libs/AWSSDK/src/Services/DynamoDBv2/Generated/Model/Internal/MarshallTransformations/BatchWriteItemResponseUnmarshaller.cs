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
    /// Response Unmarshaller for BatchWriteItem operation
    /// </summary>  
    public class BatchWriteItemResponseUnmarshaller : JsonResponseUnmarshaller    {
        public override AmazonWebServiceResponse Unmarshall(JsonUnmarshallerContext context)
        {
            BatchWriteItemResponse response = new BatchWriteItemResponse();

            context.Read();
            int targetDepth = context.CurrentDepth;
            while (context.ReadAtDepth(targetDepth))
            {
                if (context.TestExpression("ConsumedCapacity", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<ConsumedCapacity, ConsumedCapacityUnmarshaller>(ConsumedCapacityUnmarshaller.Instance);
                    response.ConsumedCapacity = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("ItemCollectionMetrics", targetDepth))
                {
                    var unmarshaller = new DictionaryUnmarshaller<string, List<ItemCollectionMetrics>, StringUnmarshaller, ListUnmarshaller<ItemCollectionMetrics, ItemCollectionMetricsUnmarshaller>>(StringUnmarshaller.Instance, new ListUnmarshaller<ItemCollectionMetrics, ItemCollectionMetricsUnmarshaller>(ItemCollectionMetricsUnmarshaller.Instance));
                    response.ItemCollectionMetrics = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("UnprocessedItems", targetDepth))
                {
                    var unmarshaller = new DictionaryUnmarshaller<string, List<WriteRequest>, StringUnmarshaller, ListUnmarshaller<WriteRequest, WriteRequestUnmarshaller>>(StringUnmarshaller.Instance, new ListUnmarshaller<WriteRequest, WriteRequestUnmarshaller>(WriteRequestUnmarshaller.Instance));
                    response.UnprocessedItems = unmarshaller.Unmarshall(context);
                    continue;
                }
            }

            return response;
        }

        public override AmazonServiceException UnmarshallException(JsonUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            ErrorResponse errorResponse = JsonErrorResponseUnmarshaller.GetInstance().Unmarshall(context);
            if (errorResponse.Code != null && errorResponse.Code.Equals("InternalServerError"))
            {
                return new InternalServerErrorException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("ItemCollectionSizeLimitExceededException"))
            {
                return new ItemCollectionSizeLimitExceededException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("ProvisionedThroughputExceededException"))
            {
                return new ProvisionedThroughputExceededException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            if (errorResponse.Code != null && errorResponse.Code.Equals("ResourceNotFoundException"))
            {
                return new ResourceNotFoundException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
            }
            return new AmazonDynamoDBException(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
        }

        private static BatchWriteItemResponseUnmarshaller _instance = new BatchWriteItemResponseUnmarshaller();        

        internal static BatchWriteItemResponseUnmarshaller GetInstance()
        {
            return _instance;
        }
        public static BatchWriteItemResponseUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }

    }
}
