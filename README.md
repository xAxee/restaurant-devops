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

- `fix: ...` increases the patch version (for example, `1.0.0` to `1.0.1`).
- `feat: ...` increases the minor version (for example, `1.0.0` to `1.1.0`).
- `BREAKING CHANGE: ...` in the commit footer, or `feat!: ...`, increases the major version (for example, `1.0.0` to `2.0.0`).
- Other commit types, such as `ci:`, `chore:` and `docs:`, do not trigger a release by themselves.

When a releasable Conventional Commit reaches `main`, Release Please creates the
version tag and GitHub Release automatically, without a release pull request. The
starting version is `1.0.0`; Git tags and GitHub Releases are the version source.
