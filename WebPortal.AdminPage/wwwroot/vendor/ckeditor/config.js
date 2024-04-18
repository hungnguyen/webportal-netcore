/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function(config) {
    // Define changes to default configuration here. For example:
    config.language = 'vi';
    // config.uiColor = '#AADC6E';
    config.toolbar = 'MyToolbar';

    config.toolbar_MyToolbar =
        [
            ['Source', '-', 'Format', 'Font', 'FontSize'],
            ['TextColor', 'BGColor'],
            ['Cut', 'Copy', 'Paste', 'PasteText', 'Print'],
            ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
            '/',
            ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
            ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
            ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Link', 'Unlink', 'Anchor'],
            ['Table', 'Image', 'Youtube', 'HorizontalRule', 'SpecialChar', 'Maximize','Preview', '-', 'About']
        ];
    //config.baseHref = 'http://localhost/webportal/';
    config.filebrowserUploadUrl = "/Home/CkUpload";
    config.allowedContent = true; //allow class attribute

    config.protectedSource.push(/<i[^>]*><\/i>/g); //alow <i> tag
    config.extraPlugins = 'youtube,image2';

    config.basicEntities = false;
    config.entities = false;
    config.entities_greek = false;
    config.entities_latin = false;
    config.htmlEncodeOutput = false;
    config.entities_processNumerical = false;
};
