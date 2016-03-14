using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Editor.TinyMCE;

namespace Alloy.Models.Plugins
{
    [TinyMCEPluginButton(PlugInName = "responsiveimageeditor",
        ButtonName = "responsiveimageeditor",
        GroupName = "misc",
        LanguagePath = "/admin/tinymce/plugins/responsiveimageeditorplugin",
        IconUrl = "Editor/tinymce/plugins/responsiveimageeditor/icon.png")]
    public class responsiveimageeditor
    {
    }
}