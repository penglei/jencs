define(function(){
    function FileDescriptor(parentPath, name, originURL, url, contentType, isEditable, isContentScript)
    {
        this.parentPath = parentPath;
        this.name = name;
        this.originURL = originURL;
        this.url = url;
        this.contentType = contentType;
        this.isEditable = isEditable;
        this.isContentScript = isContentScript || false;
    }

    return FileDescriptor;
});
