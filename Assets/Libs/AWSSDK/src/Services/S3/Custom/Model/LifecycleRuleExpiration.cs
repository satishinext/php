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
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace Amazon.S3.Model
{
    /// <summary>Expiration
    /// </summary>
    public class LifecycleRuleExpiration
    {
        
        private DateTime? date;
        private int? days;

        /// <summary>
        /// Indicates at what date the object is to be moved or deleted. Should be in GMT ISO 8601 Format.
        ///  
        /// </summary>
        public DateTime Date
        {
            get { return this.date ?? default(DateTime); }
            set { this.date = value; }
        }

        // Check to see if Date property is set
        internal bool IsSetDate()
        {
            return this.date.HasValue;
        }

        /// <summary>
        /// Indicates the lifetime, in days, of the objects that are subject to the rule. The value must be a non-zero positive integer.
        ///  
        /// </summary>
        public int Days
        {
            get { return this.days ?? default(int); }
            set { this.days = value; }
        }

        // Check to see if Days property is set
        internal bool IsSetDays()
        {
            return this.days.HasValue;
        }
    }
}
