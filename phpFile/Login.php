<?php
include 'Connect.php';
$EMAIL = $_POST['EMAIL'];
$password = $_POST['PASSWORD'];
$UID = $_POST['UID'];
$NICKNAME = $_POST['NICKNAME'];
$IP = $_POST['IP'];
$myIP = $_SERVER['REMOTE_ADDR'];

//mysqli_select_db($con,"user");

$checkipsql = "select IP from UserLogin where EMAIL = '$EMAIL'";
$checkip = mysqli_query($con,$checkipsql);
$rowIP = mysqli_fetch_assoc($checkip);
$ip = $rowIP['IP'];

if(empty($rowIP['IP'])){
$ipsql = "UPDATE UserLogin set IP = '".$myIP."' where EMAIL = '$EMAIL'"; 
$loadip = mysqli_query($con,$ipsql);
}

$adusrsql = "select * from UserLogin where EMAIL = '$EMAIL'"; 
$check = mysqli_query($con,$adusrsql); 

$CheckNickSql = "select * from UserLogin where EMAIL = '$EMAIL' and NICKNAME is null";
$CheckNick = mysqli_query($con,$CheckNickSql);

$CheckUIDSql = "select UID from UserLogin where EMAIL = '$EMAIL'";
$CheckUID = mysqli_query($con,$CheckUIDSql);

$LoadNickSql = "select NICKNAME from UserLogin where EMAIL = '$EMAIL'";
$LoadNick = mysqli_query($con,$LoadNickSql);


$numrows = mysqli_num_rows($check);
$nickrows = mysqli_num_rows($CheckNick);
$UIDrows = mysqli_fetch_assoc($CheckUID);
$LoadNickrows = mysqli_fetch_assoc($LoadNick);


if($numrows == 0){
echo("ID doesn't exist.");
exit;
}

else {
while($row = mysqli_fetch_assoc($check)){
$row['PASSWORD'] = str_replace(array("\r\n","\r","\n"),'',$row['PASSWORD']);
$password = str_replace(array("\r\n","\r","\n"),'',$password);
$verifypwd = password_verify($password,$row['PASSWORD']);
if($verifypwd){
$_SESSION['LoginEmail'] = $EMAIL;
}
else{
echo "Fail";
}
}
}

if($nickrows != 0 and $ip == $_SERVER['REMOTE_ADDR']){
echo "Login";
}
elseif(!empty($ip) and $ip != $_SERVER['REMOTE_ADDR']){
echo "diff";
}
else {
echo "Exist";
}

if($UIDrows !=0){
$UIDrows['UID'] = str_replace(array("/r/n","/r","/n"),'',$UIDrows['UID']);
$UID = $UIDrows['UID'];
$_SESSION['UID'] = $UID;
}
else{
echo "Fail";
}

if($LoadNickrows != 0){
$NICKNAME = $LoadNickrows['NICKNAME'];
$_SESSION['NICKNAME'] = $NICKNAME;
}

?>
