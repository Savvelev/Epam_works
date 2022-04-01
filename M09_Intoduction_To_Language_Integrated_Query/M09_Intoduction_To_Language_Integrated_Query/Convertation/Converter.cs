using System.Collections.Generic;


namespace M09_Intoduction_To_Language_Integrated_Query
{
    class Converter<T>
    {
        private IConverter<T> converter;
        
        public Converter(IConverter<T> converter)
        {
            this.converter = converter;
        }

        public IReadOnlyCollection<T> Converting()
        {
            return converter.Convert();
        }
    }
}
