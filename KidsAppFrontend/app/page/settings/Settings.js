import { View, Text, SafeAreaView, Alert } from 'react-native';
import React from 'react';
import Background from '@/app/components/background';
import styles from './styles';
import Banner from '@/app/components/Banner';
import SettingsButton from '@/app/components/settingsComponents/SettingsButton';
import { useDispatch, useSelector } from 'react-redux';
import { setMode } from '@/app/store/modeSlice';
import useTheme from '@/app/hooks/useTheme';

const Settings = ({ navigation }) => {
  const dispatch = useDispatch();
  const currentMode = useSelector((state) => state.mode.currentMode);
  const themeColors = useTheme();

  const userName = useSelector((state) => state.user.userName);

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

  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>
        <Banner bannerTitle={userName} />
        <View style={styles.modeContainer}>
          <Text style={[styles.modeText, { color: themeColors.textColor }]}>Mode</Text>
        </View>
        <View style={styles.buttonContainer}>
          <SettingsButton
            onPress={handleBoyMode}
            buttonTitle={'Boy'}
            theme={'BoyButton'}
            isActive={currentMode === 'Boy'}
          />
          <SettingsButton
            onPress={handleGirlMode}
            buttonTitle={'Girl'}
            theme={'GirlButton'}
            isActive={currentMode === 'Girl'}
          />
        </View>
        <SettingsButton buttonTitle="Logout" onPress={handleLogout} theme={'GirlButton'} isActive={currentMode === 'Girl'} />
      </SafeAreaView>
    </Background>
  );
};

export default Settings;