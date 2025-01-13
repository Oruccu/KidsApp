import React, { useState } from 'react';
import { View, SafeAreaView } from 'react-native';
import { useNavigation } from '@react-navigation/native';

import Background from '@/app/components/background';
import Button from '@/app/components/button';
import Input from '@/app/components/input';
import AuthTextButton from '@/app/components/authTextButton';
import styles from './styles';

const SignIn = () => {
  const navigation = useNavigation();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  // Sign In butonuna basıldığında yapılacaklar
  function handleSignIn() {
    console.log('Sign In button pressed. Add your sign-in logic here.');
    //SignIn
  }

  // "Forgot Password?" tıklandığında
  function forgotPassword() {
    console.log('Forgot password tapped.');
    navigation.navigate('ResetPassword'); 
  }

  // "Don’t have an account yet? Sign Up" tıklandığında
  function goParentSignUp() {
    console.log('Navigating to ParentSignUp screen...');
    navigation.navigate('ChildSignUp'); 
  }

  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Input
          placeholder="Username"
          onChangeText={setUsername}
          value={username}
          iconName="user"
        />
        <Input
          placeholder="Password"
          onChangeText={setPassword}
          value={password}
          iconName="key"
          secureTextEntry
        />
        <View style={styles.forgotPassword}>
          <AuthTextButton
            text="Forgot Password?"
            onPress={forgotPassword}
          />
        </View>
        <Button
          onPress={handleSignIn}
          ButtonName="Sign In"
        />
        <AuthTextButton
          text="Don’t have an account yet? Sign Up"
          onPress={goParentSignUp}
        />
      </SafeAreaView>
    </Background>
  );
};

export default SignIn;
