# CloudCustomerManagement

This project demonstrates an approach to [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) that also would be suitable to be refactored into an approach to [CQRS](https://martinfowler.com/bliki/CQRS.html)(Command Query Responsibility Segregation).
This architecture is well suited to providing highly decoupled and testable systems.

This project makes use of Google Cloud Platform's [Datastore](https://cloud.google.com/datastore/) to provide a NoSQL database implementation with ASP.NET Core WebAPI as the service interface.

To run this, you will need the Datastore Emulator or need to create credentials for a Datastore service hosted in Google Cloud. See more information [here](https://cloud.google.com/datastore/docs/datastore-api-tutorial). By default, this assumes you will use a Datastore service in Google Cloud. To provide credentials for this, add a credentials file in the solution root named `firebasekey.json`. See [here](https://cloud.google.com/datastore/docs/datastore-api-tutorial#creating_an_authorized_service_object) for more information on creating credentials.

## Major TODOs

1. Logging
1. Identity Management (w/GCP)
1. Configuration Management
1. Add a search get to the customer controller.
1. Add a customer relationship manager and generate notifications when customer is updated. Do this as a separate service.