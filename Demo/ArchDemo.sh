#!/bin/sh
echo -ne '\033c\033]0;ArchDemo\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/ArchDemo.x86_64" "$@"
