<?php
include 'Connect.php';
$EMAIL = $_SESSION['LoginEmail'];

$delipsql = "update UserLogin set IP = NULL where EMAIL = '$EMAIL'";
mysqli_query($con,$delipsql);

session_destroy();

mysqli_close($con);
?>
