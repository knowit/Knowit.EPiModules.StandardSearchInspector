using System;
using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Alloy.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(String), UIHint = "NewTextarea")]
    public class NewTextareaEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            ClientEditingClass = "alloy/editors/NewTextarea";

            base.ModifyMetadata(metadata, attributes);
        }
    }
}