# ECommerce
Cross-platform .NET real world e-commerce sample microservices and container based application that runs on Linux Windows and macOS. Powered by .NET 9, Docker Containers, Aspire, DDD, Ports and Adapters layered architecture, gRPC, Redis, MSSql, MongoDb, Rabbitmq.


## Prerequisites

- Install [Visual Studio 2022 version 17.10 or newer](https://visualstudio.microsoft.com/downloads/).
    - Select the following workloads:
        - `ASP.NET and web development` workload.
        - `.NET Aspire SDK` component in `Individual components`.
      

- Install & start Docker Desktop(https://docs.docker.com/engine/install/)
- Clone the ECommerce repository: https://gitlab.com/afshin-imanizadeh/ecommerce

## Running the solution

> [!WARNING]
> Remember to ensure that Docker is started

* Run the application from Visual Studio:
- Open the `ECommerce.sln` file in Visual Studio
- Ensure that `ECommerce.AppHost.csproj` is your startup project
- Hit Ctrl-F5 to launch Aspire

* Or run the application from your terminal:
```powershell
dotnet run --project src/ECommerce.AppHost/ECommerce.AppHost.csproj
```

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a merge request.
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/NewFeature`)
3. Commit your Changes (`git commit -m 'Add some NewFeature'`)
4. Push to the Branch (`git push origin feature/NewFeature`)
5. Create a Merge Request


<!-- CONTACT -->
## Contact

Afshin Imanizadeh - (https://www.linkedin.com/in/imanizadeh/) - a.imanizadeh@outlook.com

<!-- LICENSE -->
## License

See `LICENSE` for more information.


## Project status
- Current Status: 🚀 Active Development
- Last Update: [2025-01-28]

<!-- Technical debt -->
## Technical debt

- [ ] After deploying and running the project, monitor the database and queries, and apply appropriate indexes if needed.
- [ ] It is necessary to use the outbox pattern in future development phases to ensure that events are reliably placed on the queue.
- [ ] In future development phases, it is necessary to use the inbox pattern to ensure that events are not processed more than once or redundantly on the consumer side.
- [ ] In the next development phase, it is necessary to version the events so that consumers have the opportunity to adapt to changes made to the events on the producer side, ensuring they do not encounter errors.
- [ ] In future development phases, it is necessary to use versioning for API endpoints so that clients have the opportunity to adapt to the new changes and avoid errors.
- [ ] In the later phases of development, the database of the inventory service can be separated to give this system more independence from other services. This way, issues in the product management service database will not affect the inventory service's ability to function properly.
- [ ] In the next phase of development, it is recommended that if there is more than one user in the back-office control panel, optimistic locking should be used on database table records to prevent data corruption during data changes.
- [ ] In the next phase of development, it is recommended to use sequential GUID for the IDs of database tables.
- [ ] In the later phases of development, a new section should be added to the shared framework to handle repetitive tasks in the use case layer, such as MediatR Behaviors, so that they can be reused across all projects instead of being duplicated.
- [ ] In the later phases of development, a new section should be added to the shared framework to handle repetitive tasks across different layers, allowing them to be reused in all projects instead of being duplicated.
