#!/bin/bash
# 第三行第十列开始循环输出
tput cup 3 10
tput sc
printf "%s:" "Type word"
read -r word
tput rc
tput ed

printf "%s:" "Type word(again)"
read -r word_confirmation
