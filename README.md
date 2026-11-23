# restaurant-devops"

## PostgreSQL

Start the local database with Docker Compose:

```sh
docker compose up -d postgres
```

The API uses the `RestaurantDatabase` connection string. Development defaults
to the local Compose database; set `ConnectionStrings__RestaurantDatabase` in
the environment to override it. The API creates the database schema on startup.
