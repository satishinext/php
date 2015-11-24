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
using System.Linq;

using Amazon.DynamoDBv2.Model;
using System.IO;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// A DynamoDBEntry that represents a DynamoDB null (NULL) type.
    /// </summary>
    public class DynamoDBNull : DynamoDBEntry
    {
        public DynamoDBNull() { }

        public static readonly DynamoDBNull Null = new DynamoDBNull();

        #region Overrides

        internal override AttributeValue ConvertToAttributeValue(AttributeConversionConfig conversionConfig)
        {
            return new AttributeValue
            {
                NULL = true
            };
        }

        public override object Clone()
        {
            return new DynamoDBNull();
        }

        public override bool Equals(object obj)
        {
            var otherNull = obj as DynamoDBNull;
            if (otherNull == null)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
        
        #endregion
    }
}
