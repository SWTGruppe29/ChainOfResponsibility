using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class SeparationCondition : ICondition
    {
        private int _verticalSeparationCondition;
        private int _horizontalSeparationCondition;

        public SeparationCondition(int verticalSeparationCondition, int horizontalSeparationCondition)
        {
            _verticalSeparationCondition = verticalSeparationCondition;
            _horizontalSeparationCondition = horizontalSeparationCondition;
        }

        public int getVerticalSeparationCondition()
        {
            return _verticalSeparationCondition;
        }

        public int getHorizontalSeparationCondition()
        {
            return _horizontalSeparationCondition;
        }
    }
}
