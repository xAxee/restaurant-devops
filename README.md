# restaurant-devops

## PostgreSQL

Start the local database with Docker Compose:

```sh
docker compose up -d postgres
```

The API uses the `RestaurantDatabase` connection string. Development defaults
to the local Compose database; set `ConnectionStrings__RestaurantDatabase` in
the environment to override it. The API creates the database schema on startup.

## Versioning and releases

Commits merged into `main` follow [Conventional Commits](https://www.conventionalcommits.org/):

- `fix: ...` increases the patch version (for example, `0.1.0` to `0.1.1`).
- `feat: ...` increases the minor version (for example, `0.1.0` to `0.2.0`).
- `BREAKING CHANGE: ...` in the commit footer, or `feat!: ...`, increases the major version (for example, `0.1.0` to `1.0.0`).
- Other commit types, such as `ci:`, `chore:` and `docs:`, do not trigger a release by themselves.
