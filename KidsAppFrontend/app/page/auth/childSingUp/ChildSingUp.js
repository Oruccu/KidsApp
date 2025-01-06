import { View, Text, SafeAreaView } from 'react-native';
import React, { useState } from 'react';
import Background from '@/app/components/background';
import Button from '@/app/components/button';
import styles from './styles'
import Input from '@/app/components/input'
const ChildSingUp = () => {
  const [user, setUser] = useState("");
  const [mail, setMail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  function ParentSingUp() {
    console.log("Navigating to Parent Sign Up...");
  }

  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Input
          placeholder={'Username'}
          onChangeText={setUser}
          value={user} 
          iconName={'user'}
        />
        <Input
          placeholder={'E-Mail'}
          onChangeText={setMail}
          value={mail} 
          iconName={'mail'}
        />
        <Input
          placeholder={'Password'}
          onChangeText={setPassword}
          value={password} 
          iconName={'key'}
        />
        <Input
          placeholder={'Password'}
          onChangeText={setConfirmPassword}
          value={confirmPassword} 
          iconName={'key'}
        />
        <Button
          onPress={ParentSingUp}
          ButtonName={"Child Sign Up"} 
        />
      </SafeAreaView>
    </Background>
  );
};

export default ChildSingUp;
