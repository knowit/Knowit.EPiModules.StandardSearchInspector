(function (tinymce) {
    tinymce.create('knowit.plugins.responsiveimageeditor', {

        init: function (editor, url) {

            editor.addButton('responsiveimageeditor', { /* !important that this matches attribute in c# class */
                //title: 'responsiveimageeditor.responsiveimageeditor_desc',
                image: url + '/icon.png', /* Path to image in TinyMCE */
                onclick: function () {

                    //dojo er tilgjengelig. 
                    //console.log(dojo);

                    // Open window
                    //debugger;
                    var hideUnderlay = function () {
                        $(".dijitDialogUnderlayWrapper").hide(); //skjuler underlay for popup vindu fra dojo. dette er en hack. 
                    };

                    var dialogURL = '/Util/Editor/tinymce/plugins/responsiveimageeditor/responsiveimageeditor.html';
                    editor.windowManager.open({
                        url: dialogURL,
                        showLoadingOverlay: 0 //test alle muligheter. ser ikke ut til at den skjuler overlay.
                        //onCallback: hideUnderlay //denne fungerte ikke. kjører sannsynligvis når vinduet lukker seg.
                    });
                    
                }
            });
        },

        getInfo: function () {
            return {
                longname: 'responsiveimageeditor',
                author: 'Knowit Reaktor Oslo',
                authorurl: 'http://www.knowit.no',
                infourl: 'http://www.knowit.no',
                version: tinymce.majorVersion + "." + tinymce.minorVersion
            };
        }
    });
    tinymce.PluginManager.add('responsiveimageeditor', knowit.plugins.responsiveimageeditor);
})(tinymce);