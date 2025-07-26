# File Stream

## Blob

```js
// type MIME Type https://www.iana.org/assignments/media-types/media-types.xhtml
new Blob(array, { type: "application/octet-stream" });
URL.createObjectURL(File / Blob / MediaSource);
```

## File

```js
document.querySelector("input[type=file]").files[0];
```

`<input type="file">` 元素选择文件后返回的 FileList 对象，或者拖拽生成的 DataTransfer 对象

## FileReader

异步读取文件内容

- readAsArrayBuffer(Blob / File)
- readAsDataURL(Blob / File)
- readAsText(Blob / File)

```js
async function test(file) {
  const preview = document.querySelector("img");
  return await new Promise((resolve, reject) => {
    const reader = Object.assign(new FileReader(), {
      onload: () => {
        preview.src = reader.result;
        resolve();
      },
      progress: ({ loaded, total }) => {},
      onerror: () => reject(reader.error),
    });
    reader.readAsDataURL(file);
  });
}

export async function loadImage(image) {
  return await new Promise((resolve, reject) => {
    const img = new Image();
    img.src = image;
    Object.assign(img, {
      onload: () => {
        resolve(img);
      },
      onerror: (error) => {
        reject(error);
      },
    });
  });
}
```
