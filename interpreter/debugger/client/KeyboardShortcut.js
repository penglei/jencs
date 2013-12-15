/*
 * Copyright (C) 2009 Apple Inc. All rights reserved.
 * Copyright (C) 2009 Google Inc. All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1.  Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 * 2.  Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 * 3.  Neither the name of Apple Computer, Inc. ("Apple") nor the names of
 *     its contributors may be used to endorse or promote products derived
 *     from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY APPLE AND ITS CONTRIBUTORS "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL APPLE OR ITS CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

define(function (require) {

var UIUtils = require("UIUtils");

/**
 * @constructor
 */
function KeyboardShortcut()
{
}

/**
 * Constants for encoding modifier key set as a bit mask.
 * @see #_makeKeyFromCodeAndModifiers
 */
KeyboardShortcut.Modifiers = {
    None: 0,   // Constant for empty modifiers set.
    Shift: 1,
    Ctrl: 2,
    Alt: 4,
    Meta: 8,   // Command key on Mac, Win key on other platforms.
    get CtrlOrMeta()
    {
        // "default" command/ctrl key for platform, Command on Mac, Ctrl on other platforms
        return UIUtils.isMac() ? this.Meta : this.Ctrl;
    }
};

/** @typedef {{code: number, name: (string|Object.<string, string>)}} */
KeyboardShortcut.Key;

/** @type {!Object.<string, !KeyboardShortcut.Key>} */
KeyboardShortcut.Keys = {
    Backspace: { code: 8, name: "\u21a4" },
    Tab: { code: 9, name: { mac: "\u21e5", other: "Tab" } },
    Enter: { code: 13, name: { mac: "\u21a9", other: "Enter" } },
    Esc: { code: 27, name: { mac: "\u238b", other: "Esc" } },
    Space: { code: 32, name: "Space" },
    PageUp: { code: 33,  name: { mac: "\u21de", other: "PageUp" } },      // also NUM_NORTH_EAST
    PageDown: { code: 34, name: { mac: "\u21df", other: "PageDown" } },   // also NUM_SOUTH_EAST
    End: { code: 35, name: { mac: "\u2197", other: "End" } },             // also NUM_SOUTH_WEST
    Home: { code: 36, name: { mac: "\u2196", other: "Home" } },           // also NUM_NORTH_WEST
    Left: { code: 37, name: "\u2190" },           // also NUM_WEST
    Up: { code: 38, name: "\u2191" },             // also NUM_NORTH
    Right: { code: 39, name: "\u2192" },          // also NUM_EAST
    Down: { code: 40, name: "\u2193" },           // also NUM_SOUTH
    Delete: { code: 46, name: "Del" },
    Zero: { code: 48, name: "0" },
    F1: { code: 112, name: "F1" },
    F2: { code: 113, name: "F2" },
    F3: { code: 114, name: "F3" },
    F4: { code: 115, name: "F4" },
    F5: { code: 116, name: "F5" },
    F6: { code: 117, name: "F6" },
    F7: { code: 118, name: "F7" },
    F8: { code: 119, name: "F8" },
    F9: { code: 120, name: "F9" },
    F10: { code: 121, name: "F10" },
    F11: { code: 122, name: "F11" },
    F12: { code: 123, name: "F12" },
    Semicolon: { code: 186, name: ";" },
    Plus: { code: 187, name: "+" },
    Comma: { code: 188, name: "," },
    Minus: { code: 189, name: "-" },
    Period: { code: 190, name: "." },
    Slash: { code: 191, name: "/" },
    Apostrophe: { code: 192, name: "`" },
    Backslash: { code: 220, name: "\\" },
    SingleQuote: { code: 222, name: "\'" },
    H: { code: 72, name: "H" },
    Ctrl: { code: 17, name: "Ctrl" },
    Meta: { code: 91, name: "Meta" },
    Tilde: { code: 192, name: "Tilde" },
    get CtrlOrMeta()
    {
        // "default" command/ctrl key for platform, Command on Mac, Ctrl on other platforms
        return UIUtils.isMac() ? this.Meta : this.Ctrl;
    },
};

/**
 * Creates a number encoding keyCode in the lower 8 bits and modifiers mask in the higher 8 bits.
 * It is useful for matching pressed keys.
 *
 * @param {number|string} keyCode The Code of the key, or a character "a-z" which is converted to a keyCode value.
 * @param {number=} modifiers Optional list of modifiers passed as additional paramerters.
 * @return {number}
 */
KeyboardShortcut.makeKey = function(keyCode, modifiers)
{
    if (typeof keyCode === "string")
        keyCode = keyCode.charCodeAt(0) - 32;
    modifiers = modifiers || KeyboardShortcut.Modifiers.None;
    return KeyboardShortcut._makeKeyFromCodeAndModifiers(keyCode, modifiers);
}

/**
 * @param {KeyboardEvent} keyboardEvent
 * @return {number}
 */
KeyboardShortcut.makeKeyFromEvent = function(keyboardEvent)
{
    var modifiers = KeyboardShortcut.Modifiers.None;
    if (keyboardEvent.shiftKey)
        modifiers |= KeyboardShortcut.Modifiers.Shift;
    if (keyboardEvent.ctrlKey)
        modifiers |= KeyboardShortcut.Modifiers.Ctrl;
    if (keyboardEvent.altKey)
        modifiers |= KeyboardShortcut.Modifiers.Alt;
    if (keyboardEvent.metaKey)
        modifiers |= KeyboardShortcut.Modifiers.Meta;
    return KeyboardShortcut._makeKeyFromCodeAndModifiers(keyboardEvent.keyCode, modifiers);
}

/**
 * @param {KeyboardEvent} event
 * @return {boolean}
 */
KeyboardShortcut.eventHasCtrlOrMeta = function(event)
{
    return UIUtils.isMac() ? event.metaKey && !event.ctrlKey : event.ctrlKey && !event.metaKey;
}

/**
 * @param {Event} event
 * @return {boolean}
 */
KeyboardShortcut.hasNoModifiers = function(event)
{
    return !event.ctrlKey && !event.shiftKey && !event.altKey && !event.metaKey;
}

/** @typedef {{key: number, name: string}} */
KeyboardShortcut.Descriptor;

/**
 * @param {string|KeyboardShortcut.Key} key
 * @param {number=} modifiers
 * @return {KeyboardShortcut.Descriptor}
 */
KeyboardShortcut.makeDescriptor = function(key, modifiers)
{
    return {
        key: KeyboardShortcut.makeKey(typeof key === "string" ? key : key.code, modifiers),
        name: KeyboardShortcut.shortcutToString(key, modifiers)
    };
}

/**
 * @param {string|KeyboardShortcut.Key} key
 * @param {number=} modifiers
 * @return {string}
 */
KeyboardShortcut.shortcutToString = function(key, modifiers)
{
    return KeyboardShortcut._modifiersToString(modifiers) + KeyboardShortcut._keyName(key);
}

/**
 * @param {string|KeyboardShortcut.Key} key
 * @return {string}
 */
KeyboardShortcut._keyName = function(key)
{
    if (typeof key === "string")
        return key.toUpperCase();
    if (typeof key.name === "string")
        return key.name;
    return key.name[UIUtils.platform()] || key.name.other || '';
}

/**
 * @param {number} keyCode
 * @param {?number} modifiers
 * @return {number}
 */
KeyboardShortcut._makeKeyFromCodeAndModifiers = function(keyCode, modifiers)
{
    return (keyCode & 255) | (modifiers << 8);
};

/**
 * @param {number|undefined} modifiers
 * @return {string}
 */
KeyboardShortcut._modifiersToString = function(modifiers)
{
    const cmdKey = "\u2318";
    const optKey = "\u2325";
    const shiftKey = "\u21e7";
    const ctrlKey = "\u2303";

    var isMac = UIUtils.isMac();
    var res = "";
    if (modifiers & KeyboardShortcut.Modifiers.Ctrl)
        res += isMac ? ctrlKey : "Ctrl + ";
    if (modifiers & KeyboardShortcut.Modifiers.Alt)
        res += isMac ? optKey : "Alt + ";
    if (modifiers & KeyboardShortcut.Modifiers.Shift)
        res += isMac ? shiftKey : "Shift + ";
    if (modifiers & KeyboardShortcut.Modifiers.Meta)
        res += isMac ? cmdKey : "Win + ";

    return res;
};

KeyboardShortcut.SelectAll = KeyboardShortcut.makeKey("a", KeyboardShortcut.Modifiers.CtrlOrMeta);

return KeyboardShortcut

});