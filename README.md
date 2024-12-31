# BiLink

A command line tool that manages symbolic links.

## Usage

`bilink <command>`

Commands:
- `link`
- `unlink`

### Link

`bilink link <path> <target> [OPTIONS]`

Options:
- `--force`

Create symbolic link at path pointing to target. If path exists and target does not exists then move it to target, unless force option is set.

### Unlink

`bilink unlink <path>`

Attempt to remove symbolic link at path, then move the automatically resolved target directory to it.