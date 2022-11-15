# Simple-Rest-Api-Test-Tool
Tool for simple tests of apis rest.
I was in need of a simple and fast tool for testing sequences of requests in a Rest WebAPI. That's when I thought of implementing this tool. The idea is to configure the sequence of test steps in an xml file.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<TestCase>
  <step method="get" uri="http://localhost:5000/api/test/value/1">
    <request></request>
    <expected>
      {
        "id": 1,
        "value": null,
        "status": "not found"
      }
    </expected>
  </step>
  <step method="post" uri="http://localhost:5000/api/test/value">
    <request>
      {
      "id": 1,
      "value": "The quick brown fox jumps over the lazy dog.",
      "status": null
      }
    </request>
    <expected>
      {
      "id": 1,
      "value": "The quick brown fox jumps over the lazy dog.",
      "status": "inserted"
      }
    </expected>
  </step>
  <step method="get" uri="http://localhost:5000/api/test/value/1">
    <request></request>
    <expected>
      {
      "id": 1,
      "value": "The quick brown fox jumps over the lazy dog.",
      "status": "finded"
      }
    </expected>
  </step>
</TestCase>
```
I think the above example code speaks for itself. To exemplify, I implemented a simplified CRUD with LiteDB that can be executed together with the two sample tests that are in this repository.
