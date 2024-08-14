# document

[GeoJSON](https://geojson.org/)

## introduction

定义

- 选项 geometry.type 是可选的区分大小写的值:
  - Point
  - MultiPoint
  - LineString
  - MultiLineString
  - Polygon
  - MultiPolygon
  - GeometryCollection
- 选项 GeoJSON types 是可选的区分大小写的值:
  - Feature
  - FeatureCollection
  - 上面 geometry.type 的可选值.
- FeatureCollection 和 GeometryCollection 中的  features 和 geometries 的成员是标准有序的数组

Example:

```json
{
  "type": "FeatureCollection",
  "features": [
    {
      "type": "Feature",
      "geometry": {
        "type": "Point",
        "coordinates": [10, 10]
      },
      "properties": {
        "prop0": "value0"
      }
    },
    {
      "type": "Feature",
      "geometry": {
        "type": "LineString",
        "coordinates": [[10, 10],[20, 20]]
      },
      "properties": {
        "prop0": "value0",
        "prop1": true,
        "prop2": 3.8
      }
    }
  ]
}
```

映射地理坐标:

- geo:lat 维度, lon 经度, alt 海拔
- GeoJSON: {"type": "Point", "coordinates": [lon, lat, alt]}

## GeoJSON Object

- Geometry Object
- Feature Object
  1. "type" 选项的值是 "Feature"
  2. 有一个 "geometry" 的成员
  3. 有一个 "properties" 的成员
  4. 功能需要标识符时，添加一个 id 成员
- FeatureCollection Object
  1. 有一个 "features" 的成员，值是 JSON Array

## Coordinate Reference System

World Geodetic System 1984 (WGS 84), 十进制 longitude 和 latitude

## Bounding Box

Geometries, Features, 或 FeatureCollections 有一个 "bbox" 成员，值是长度为 2*n 的数组
