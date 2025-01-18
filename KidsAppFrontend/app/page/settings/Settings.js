import React, { useState } from 'react';
import { View, Text, SafeAreaView, Alert } from 'react-native';
import { useDispatch, useSelector } from 'react-redux';
import Background from '@/app/components/background';
import Banner from '@/app/components/Banner';
import SettingsButton from '@/app/components/settingsComponents/SettingsButton';
import Input from '@/app/components/input';
import { setMode } from '@/app/store/modeSlice';
import useTheme from '@/app/hooks/useTheme';
import color from '@/app/styles/color';
import UserButton from '@/app/components/settingsComponents/UserButton';
import styles from './styles';

const Settings = ({ navigation }) => {
  const dispatch = useDispatch();
  const currentMode = useSelector((state) => state.mode.currentMode);
  const themeColors = useTheme();
  const userName = useSelector((state) => state.user.userName);

  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  // Mod durumuna göre dinamik renkler:
  const buttonBackgroundColor =
    currentMode === 'Boy'
      ? color.BoyColors.Primary
      : color.GirlColors.Primary;

  // Input container'ın arka plan rengi:
  const inputBackgroundColor =
    currentMode === 'Boy'
      ? color.BoyColors.Primary
      : color.GirlColors.Primary;

  // Buton üzerindeki metin rengi:
  const buttonTextColor =
    currentMode === 'Boy'
      ? color.GirlColors.Primary
      : color.BoyColors.Primary;

  const handleBoyMode = () => {
    dispatch(setMode('Boy'));
  };

  const handleGirlMode = () => {
    dispatch(setMode('Girl'));
  };

  const handleLogout = async () => {
    try {
      Alert.alert('Success', 'You have been logged out.');
      navigation.replace('SignIn');
    } catch (error) {
      Alert.alert('Error', error.message);
    }
  };

  const handleDeleteAccount = async () => {
    try {
      await api.delete('/api/auth/deleteAccount');
      await AsyncStorage.removeItem('token');
      Alert.alert('Account deleted');
      navigation.replace('SignIn');
    } catch (error) {
      Alert.alert('Error', error.message);
    }
  };

  return (
    <Background showOverlay={true} mode={currentMode}>
      <SafeAreaView style={styles.container}>
        <Banner bannerTitle={userName} />

        <View style={styles.modeContainer}>
          <Text
            style={[
              styles.modeText,
              {
                color:
                  currentMode === 'Boy'
                    ? themeColors.boyTextColor
                    : themeColors.girlTextColor,
              },
            ]}
          >
            Mode
          </Text>
        </View>

        <View style={styles.buttonContainer}>
          <SettingsButton
            onPress={handleBoyMode}
            buttonTitle="Boy"
            theme="BoyButton"
            isActive={currentMode === 'Boy'}
          />
          <SettingsButton
            onPress={handleGirlMode}
            buttonTitle="Girl"
            theme="GirlButton"
            isActive={currentMode === 'Girl'}
          />
        </View>

        <View style={[styles.inputContainer, { backgroundColor: inputBackgroundColor }]}>
          <Input
            placeholder="Username"
            onChangeText={setUsername}
            value={username}
            iconName="user"
          />
          <UserButton
            onPress={() => Alert.alert('Login successful')}
            title="Login"
            customTextStyle={{ color: buttonTextColor }}
          />
        </View>

        <View style={styles.buttonContainerTwo}>

          <UserButton
            onPress={handleLogout}
            title="LogOut"
            buttonBackgroundColor={buttonBackgroundColor}
            customTextStyle={{ color: buttonTextColor }}
          />
          <UserButton
            onPress={handleDeleteAccount}
            title="Delete"
            buttonBackgroundColor={buttonBackgroundColor}
            customTextStyle={{ color: buttonTextColor }}
          />
        </View>
      </SafeAreaView>
    </Background>
  );
};

export default Settings;
