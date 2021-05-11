namespace ChapterMaster.UI
{
    public class VerticalScrollbar : Slider
    {
        public VerticalScrollbar(string sliderTextureId, int initialValue, int minValue, int maxValue, float increment = 1, MouseHandler onClick = null, SliderHandler outOfFocus = null, SliderHandler updateValue = null) : base(sliderTextureId, initialValue, minValue, maxValue, increment, onClick, outOfFocus, updateValue)
        {
        }

        public VerticalScrollbar(string sliderTextureId, int initialValue, Align.Align align, int minValue, int maxValue, float increment = 1, MouseHandler onClick = null, SliderHandler outOfFocus = null, SliderHandler updateValue = null) : base(sliderTextureId, initialValue, align, minValue, maxValue, increment, onClick, outOfFocus, updateValue)
        {
        }
    }
}