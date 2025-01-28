
# **Architecture decision record for the E-commerce**

**Date:** [2025-01-29]  
**Status:** Accepted  
**Created By:** Afshin Imanizadeh
---

## **Context**  
This ADR outlines the architectural decisions made for the development of the e-commerce hosted at [ECommerce](https://gitlab.com/afshin-imanizadeh/ecommerce). The project aims to create a scalable, modular, and maintainable e-commerce platform with key features like product management, inventory management, catalog service and integrations with external services.  

---

## **Decision**  

### 1. **Microservices Architecture**  
- The platform will adopt a **microservices-based architecture** to ensure scalability and allow independent development and deployment of features.  
- Each core feature (product management, inventory management, product catalog) will be developed as a separate service.  

### 2. **Event-Driven Communication**  
- Integration between services will rely on an **event-driven architecture**, using integration events to decouple services and enable asynchronous communication.  


### 3. **API Gateway**  
- A single **API Gateway** will serve as the entry point to route requests to backend services, handle protocol translation, and enforce authentication.  


### 4. **Database Per Service**  
- Product management and catalog services will maintain its own database to enforce loose coupling and ensure data integrity.  


### 5. **CI/CD Pipeline**  
- The repository integrates a **GitLab CI/CD pipeline** to automate testing and building  applications.  

### 6. **MonoDb Database**  
- MongoDB can scale horizontally or vertically to handle large amounts of data. It can also be distributed across multiple servers to reduce downtime and MongoDB's JSON-like format maps to native objects in most modern programming languages. This makes it easier for developers to work with. 

### 7. **Redis Database**  
- Redis cache is used to store frequently accessed data in memory, which improves application performance and reduces database load.

### 8. **SQL Server Database** 
SQL Server provides users with a range of cutting-edge features that recover and restore data that's been damaged or lost; they can even recover a complete dataset. SQL Server's Database Engine oversees data storage and aids with executing user queries and demands, such as files, transactions, or indexes.

## **Consequences**  

### **Benefits**  
1. **Scalability**: Microservices enable independent scaling of services based on workload.  
2. **Resilience**: Decoupled services ensure that a failure in one service doesn’t impact others.  
3. **Developer Velocity**: Teams can work on independent services without conflicts.  
4. **Maintainability**: Smaller, focused services are easier to debug and enhance.  
5. **Client Adaptability**: Versioning ensures that clients and consumers can adapt to changes incrementally.  

### **Challenges**  
1. **Operational Complexity**: Managing multiple services introduces overhead in deployment and monitoring.  
2. **Eventual Consistency**: The event-driven architecture may require managing consistency across services.  
3. **Testing**: Testing integration and communication between services will require additional effort.  

---

## **Next Steps**  

1. It is necessary to use the outbox pattern in future development phases to ensure that events are reliably placed on the queue.

2. In future development phases, it is necessary to use the inbox pattern to ensure that events are not processed more than once or redundantly on the consumer side.

3. In the next development phase, it is necessary to version the events so that consumers have the opportunity to adapt to changes made to the events on the producer side, ensuring they do not encounter errors.

4. In future development phases, it is necessary to use versioning for API endpoints so that clients have the opportunity to adapt to the new changes and avoid errors.

5. In the later phases of development, the database of the inventory service can be separated to give this system more independence from other services. This way, issues in the product management service database will not affect the inventory service's ability to function properly.

6. In the next phase of development, it is recommended that if there is more than one user in the back-office control panel, optimistic locking should be used on database table records to prevent data corruption during data changes.

7. In the next phase of development, it is recommended to use sequential GUID for the IDs of database tables.

 8. In the later phases of development, a new section should be added to the shared framework to handle repetitive tasks in the use case layer, such as MediatR Behaviors, so that they can be reused across all projects instead of being duplicated.

 9. In the later phases of development, a new section should be added to the shared framework to handle repetitive tasks across different layers, allowing them to be reused in all projects instead of being duplicated.  

