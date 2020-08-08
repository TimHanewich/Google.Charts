using System;
using System.Collections.Generic;

namespace TimHanewich.Google.Charts
{
    public interface IChartRequestComponent
    {
        KeyValuePair<string, string>[] GenerateFormContent();
    }
}
