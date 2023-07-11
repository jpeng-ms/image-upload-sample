# image-upload-sample


## upload endpoints


| endpoints  |  notes  |
|---|---|
| http://localhost:27813/api/v1/images/body  |  body needs to have image to be base 64 encoded in JSON format such as ` { "file": "<BASE_64_STRING>" } ` |
|  http://localhost:27813/api/v1/images/formdata |  formdata needs to have a key named "file" with image binary  |
|  http://localhost:27813/api/v1/images/binary  |  image binary in payload body  |

