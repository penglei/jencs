/*
 * Copyright (C) 2012 Google Inc. All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 * 1. Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above
 * copyright notice, this list of conditions and the following disclaimer
 * in the documentation and/or other materials provided with the
 * distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY GOOGLE INC. AND ITS CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL GOOGLE INC.
 * OR ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
define(function(require){

//var ContextMenu = require("ContextMenu");
var PanelDescriptor = require("Panel").PanelDescriptor;
var KeyboardShortcut = require("KeyboardShortcut");

/**
 * @constructor
 * @extends {PanelDescriptor}
 * @implements {ContextMenu.Provider}
 */
function ScriptsPanelDescriptor()
{
    PanelDescriptor.call(this, "scripts", UIString("Sources"), "ScriptsPanel", "ScriptsPanel.js");
    //ContextMenu.registerProvider(this);
}

ScriptsPanelDescriptor.prototype = {
    /** 
     * @param {ContextMenu} contextMenu
     * @param {Object} target
     */
    appendApplicableItems: function(event, contextMenu, target)
    {
        var hasApplicableItems = target instanceof UISourceCode;

        if (!hasApplicableItems && target instanceof RemoteObject) {
            var remoteObject = /** @type {RemoteObject} */ (target);
            if (remoteObject.type !== "function")
                return;
        }

        this.panel().appendApplicableItems(event, contextMenu, target);
    },

    registerShortcuts: function()
    {
        var section = shortcutsScreen.section(UIString("Sources Panel"));

        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.PauseContinue, UIString("Pause/Continue"));
        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.StepOver, UIString("Step over"));
        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.StepInto, UIString("Step into"));
        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.StepIntoSelection, UIString("Step into selection"));
        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.StepOut, UIString("Step out"));

        var nextAndPrevFrameKeys = ScriptsPanelDescriptor.ShortcutKeys.NextCallFrame.concat(ScriptsPanelDescriptor.ShortcutKeys.PrevCallFrame);
        section.addRelatedKeys(nextAndPrevFrameKeys, UIString("Next/previous call frame"));

        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.EvaluateSelectionInConsole, UIString("Evaluate selection in console"));
        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.GoToMember, UIString("Go to member"));
        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.ToggleBreakpoint, UIString("Toggle breakpoint"));
        section.addAlternateKeys(ScriptsPanelDescriptor.ShortcutKeys.ToggleComment, UIString("Toggle comment"));
    },

    __proto__: PanelDescriptor.prototype
}

ScriptsPanelDescriptor.ShortcutKeys = {
    RunSnippet: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.Enter, KeyboardShortcut.Modifiers.CtrlOrMeta)
    ],

    PauseContinue: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.F8),
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.Backslash, KeyboardShortcut.Modifiers.CtrlOrMeta)
    ],

    StepOver: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.F10),
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.SingleQuote, KeyboardShortcut.Modifiers.CtrlOrMeta)
    ],

    StepInto: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.F11),
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.Semicolon, KeyboardShortcut.Modifiers.CtrlOrMeta)
    ],

    StepIntoSelection: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.F11, KeyboardShortcut.Modifiers.CtrlOrMeta),
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.F11, KeyboardShortcut.Modifiers.Shift | KeyboardShortcut.Modifiers.CtrlOrMeta)
    ],

    StepOut: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.F11, KeyboardShortcut.Modifiers.Shift),
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.Semicolon, KeyboardShortcut.Modifiers.Shift | KeyboardShortcut.Modifiers.CtrlOrMeta)
    ],

    EvaluateSelectionInConsole: [
        KeyboardShortcut.makeDescriptor("e", KeyboardShortcut.Modifiers.Shift | KeyboardShortcut.Modifiers.Ctrl)
    ],

    GoToMember: [
        KeyboardShortcut.makeDescriptor("o", KeyboardShortcut.Modifiers.CtrlOrMeta | KeyboardShortcut.Modifiers.Shift)
    ],

    ToggleBreakpoint: [
        KeyboardShortcut.makeDescriptor("b", KeyboardShortcut.Modifiers.CtrlOrMeta)
    ],

    NextCallFrame: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.Period, KeyboardShortcut.Modifiers.Ctrl)
    ],

    PrevCallFrame: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.Comma, KeyboardShortcut.Modifiers.Ctrl)
    ],

    ToggleComment: [
        KeyboardShortcut.makeDescriptor(KeyboardShortcut.Keys.Slash, KeyboardShortcut.Modifiers.CtrlOrMeta)

    ]
};
return ScriptsPanelDescriptor;
});
