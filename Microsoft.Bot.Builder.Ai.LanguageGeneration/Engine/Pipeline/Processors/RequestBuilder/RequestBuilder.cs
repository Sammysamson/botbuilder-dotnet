﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DialogFoundation.Backend.LG;
using LanguageGeneration.V2;

namespace Microsoft.Bot.Builder.AI.LanguageGeneration.Engine
{
    internal class RequestBuilder : IRequestBuilder
    {
        public ICompositeRequest BuildRequest(IList<Slot> slots)
        {
            ICompositeRequest compositeRequest = new CompositeRequest();
            IList<Slot> perRequestSlots = new List<Slot>();
            var commonSlotsDictionary = new LGSlotDictionary();

            foreach (var slot in slots)
            {
                if (slot.KeyValue.Key == "GetStateName")
                {
                    perRequestSlots.Add(slot);
                }
                else
                {
                    if (slot.Type == SlotTypeEnum.BooleanType)
                    {
                        var lgValue = new LGValue(LgValueType.BooleanType)
                        {
                            BooleanValues = new List<bool> { (bool)slot.KeyValue.Value }
                        };
                        commonSlotsDictionary.Add(slot.KeyValue.Key, lgValue);
                    }

                    else if (slot.Type == SlotTypeEnum.DateTimeType)
                    {
                        var lgValue = new LGValue(LgValueType.DateTimeType)
                        {
                            DateTimeValues = new List<DateTime> { (DateTime)slot.KeyValue.Value }
                        };
                        commonSlotsDictionary.Add(slot.KeyValue.Key, lgValue);
                    }

                    else if (slot.Type == SlotTypeEnum.FloatType)
                    {
                        var lgValue = new LGValue(LgValueType.FloatType)
                        {
                            FloatValues = new List<float> { (float)slot.KeyValue.Value }
                        };
                        commonSlotsDictionary.Add(slot.KeyValue.Key, lgValue);
                    }

                    else if (slot.Type == SlotTypeEnum.IntType)
                    {
                        var lgValue = new LGValue(LgValueType.IntType)
                        {
                            IntValues = new List<int> { (int)slot.KeyValue.Value }
                        };
                        commonSlotsDictionary.Add(slot.KeyValue.Key, lgValue);
                    }

                    else if (slot.Type == SlotTypeEnum.StringType)
                    {
                        var lgValue = new LGValue(LgValueType.StringType)
                        {
                            StringValues = new List<string> { (string)slot.KeyValue.Value }
                        };
                        commonSlotsDictionary.Add(slot.KeyValue.Key, lgValue);
                    }
                }
            }

            foreach (var slot in perRequestSlots)
            {
                var lgRequest = new LGRequest();
                var slotStringValue = (List<string>)slot.KeyValue.Value;
                var lgValue = new LGValue(LgValueType.StringType)
                {
                    StringValues = slotStringValue
                };
                lgRequest.Slots = new LGSlotDictionary();
                foreach (var commonSlot in commonSlotsDictionary)
                {
                    lgRequest.Slots.Add(commonSlot);
                }
                lgRequest.Slots.Add(slot.KeyValue.Key, lgValue);

                compositeRequest.Requests.Add(slotStringValue[0], lgRequest);
            }

            return compositeRequest;
        }
    }
}
