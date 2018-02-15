namespace System.Drawing
{
    [Flags]
    public enum FontStyle
    {
        /// <include file='doc\FontStyle.uex' path='docs/doc[@for="FontStyle.Regular"]/*' />
        /// <devdoc>
        ///    Normal text.
        /// </devdoc>
        Regular = 0,
        /// <include file='doc\FontStyle.uex' path='docs/doc[@for="FontStyle.Bold"]/*' />
        /// <devdoc>
        ///    Bold text.
        /// </devdoc>
        Bold = 1,
        /// <include file='doc\FontStyle.uex' path='docs/doc[@for="FontStyle.Italic"]/*' />
        /// <devdoc>
        ///    Italic text.
        /// </devdoc>
        Italic = 2,
        /// <include file='doc\FontStyle.uex' path='docs/doc[@for="FontStyle.Underline"]/*' />
        /// <devdoc>
        ///    Underlined text.
        /// </devdoc>
        Underline = 4,
        /// <include file='doc\FontStyle.uex' path='docs/doc[@for="FontStyle.Strikeout"]/*' />
        /// <devdoc>
        ///    Text with a line through the middle.
        /// </devdoc>
        Strikeout = 8,
    }

}
