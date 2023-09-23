#!/bin/bash

# 新行每4秒就刷新计数
echo Count:
tput sc
for count in `seq 0 6`
do	
	tput rc
	tput ed
	echo -n $count
	sleep 4
done
