 /**
 * @param {string} string
 * @param {...*} vararg
 * @return {string}
 */
function UIString(string, vararg)
{
    return String.vsprintf(string, Array.prototype.slice.call(arguments, 1));
}

