# BiLink

BiLink is a command-line tool for managing symbolic links between directories.

## Quick Start

### Installation

The easiest way to set up BiLink is to place the downloaded executable in `C:\Windows` because it is already included in the Path environment variable.

### Administrator Privileges

BiLink requires administrator privileges when modifying system directories, such as `Program Files`.

To achieve this, you can run your terminal with administrative privileges. Alternatively, you can use Windows sudo mode available in Windows 24H2 and above.

## Usage

### Create symbolic link

`bilink link -s|--source <source> -d|--destination <destination> [--delete-source] [--delete-destination] [--verbose]`

Create a symbolic link from the source to the destination. If source is not empty then move it to destination.

- `--source` Source directory where symbolic link will be created
- `--destination` Destination directory where symbolic link will point to
- `--delete-source` Delete source directory before creating symbolic link, otherwise move it
- `--delete-destination` delete destination directory if not empty, otherwise fails
- `--verbose` Print additional information

### Replace symbolic link

`bilink unlink -s <source> [--delete-source] [--verbose]`

Replace symbolic link with the directory it points to.

- `--source` Source directory where symbolic link is located
- `--delete-source` Delete directory where the symbolic link points after operation completes
- `--verbose` Print additional information