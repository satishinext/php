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

using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Get Bucket Notification Request Marshaller
    /// </summary>       
    public class GetBucketNotificationRequestMarshaller : IMarshaller<IRequest, GetBucketNotificationRequest> ,IMarshaller<IRequest,Amazon.Runtime.AmazonWebServiceRequest>
	{
		public IRequest Marshall(Amazon.Runtime.AmazonWebServiceRequest input)
		{
			return this.Marshall((GetBucketNotificationRequest)input);
		}

        public IRequest Marshall(GetBucketNotificationRequest getBucketNotificationRequest)
        {
            IRequest request = new DefaultRequest(getBucketNotificationRequest, "AmazonS3");

            request.HttpMethod = "GET";

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(getBucketNotificationRequest.BucketName));
            request.AddSubResource("notification");
            request.UseQueryString = true;
            
            return request;
        }
    }
}
    
