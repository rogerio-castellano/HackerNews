COMMENTS
- It has been adopted a minimal implementation of Clean architecture; for simplification purposes the layers have been 
separated in folders, but in production code probably it should be distributed in different .Net projects; for the same 
reason interfacas are in the same folder as the classes that depends on them.

-0 Although when running the tests the results already come in the required sorting, there was no statement in the API 
documentation about that, so it has been assumed that it may not always happen.

- A simple non distributed solution has been implemented to cache data, in a production application running in a distributed 
environment, solutions like Redis would be more suitable

- As the application may have a large number of requests only top 20 stories are stored to save memory usage.

- Requesting a network resource may fail, so it is necessary to implement retrying strategy in StoryRepository to retry getting the resource for
a limited amount of times, probably in exponential intervals

ASSUMPTIONS
- It has been assumed that data may be cached for 60 seconds without significant loss to the business; this value is configurable

