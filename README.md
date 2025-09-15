# Trivia Terminal App

A simple terminal-based trivia game built with Spectre.Console.

## Commands

### `play`

Starts a new trivia game.

**Options:**

| Option                 | Description                     | Default |
|------------------------|----------------------------------|---------|
| `-p`, `--players`      | The number of players            | `1`     |
| `-n`, `--questions`    | The number of questions          | `10`    |
| `-d`, `--difficulty`   | The level of difficulty          | `All`   |
| `-c`, `--category`     | The category of trivia           | `All`   |

**Example:**

```bash
dotnet run -- play --players 2 --questions 5 --difficulty easy --category history
```

### `categories`

List all posible categories
```bash
dotnet run -- categories
```

### `difficulties`

List all diificulties
```bash
dotnet run -- difficulties
```
