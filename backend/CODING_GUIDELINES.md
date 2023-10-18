# Coding Guidelines for Backend

The project is in protyping stage and currently we do not have strict coding guidelines.
However, we have started outlining a few things here.

## USE Exceptions for error handling, NOT result with errors
There are a few popular error handling styles:
1. Throw Exceptions (Some say exceptions are expensive so they do not use this style)
2. Return response with Success or Failure status
3. Mix of 1 and 2

We use style #1 for simplicity. We do not get millions of calls per second on our APIs and the
minor performance overhead of Exceptions does not impact us.

## USE x is null, NOT x == null
Use *is* operator to compare variables with *null*. *==* can be overloaded and can cause unexpected behaviour.

## Return Result\<T> for basic types like string, int, boolean, etc.
To confirm with valid JSON responses, do not return basic types like string, int, boolean, etc.
directly. Use *Result\<T>* which has *Value* property. *new Result\<int>(123)* will be serialized as
below.

    {
        "value": 123
    }

## Return ActionResult\<T> from API methods
You can use different return types from API Controllers: the actual object type, *IActionResult*,
*ActionResult\<T>*, etc.

For consistency, we should always use *ActionResult\<T>*.

## Return NoContent() if an action does not return any data
Usually we do not return anything from methods which update or delete data. If any method does
not return any data then return *NoContent()* from the action.

## Use property binding attributes in Commands and Queries
We have chosen to use attributes like *FromRoute*, *FromQuery* and *FromBody* in the Application layer
Commands/Queries. While this might be a violation of Clean Architecture, we prefer this method
for simplicity.

We model the Commands/Queries as below:

- Parameters read from routes and queries are created as direct properties of the record/class and
decorated with *FromRoute* and *FromQuery* attributes as required.
- Parameters read from body are created as a separate record/class. A property of this
record/class type is created in the Command/Query. This property is decorated with the *FromBody*
attribute.
