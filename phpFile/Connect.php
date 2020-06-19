<?php
$ip = 'localhost:3456';
$root = 'chanwoo';
$rootpassword = 'aa1950630q!';
$dbname = 'user';

$con = mysqli_connect($ip,$root,$rootpassword,$dbname,true);

if(!$con){
echo("connection Faild" . mysqli_connect_error());
}

mysqli_select_db($con,"user");
session_start();
?>
