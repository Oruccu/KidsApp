import { View, Text, SafeAreaView, Button } from 'react-native';
import React, { useEffect } from 'react';
import Background from '@/app/components/background';
import styles from './styles';
import Banner from '@/app/components/Banner';
import SettingsButton from '@/app/components/settingsComponents/SettingsButton';
import { useDispatch, useSelector } from 'react-redux';
import { setMode, fetchMode } from '@/app/store/modeSlice';
import useTheme from '@/app/hooks/useTheme';
import { logout } from '@/app/services/api.js';

const Settings = ({ navigation }) => { 
  const dispatch = useDispatch();
  const currentMode = useSelector((state) => state.mode.currentMode); 
  const status = useSelector((state) => state.mode.status);
  const error = useSelector((state) => state.mode.error);
  const themeColors = useTheme();

  const userName = useSelector((state) => state.user.userName);
  
  console.log(userName);
  useEffect(() => {
    dispatch(fetchMode());
  }, [dispatch]);
  
  const handleBoyMode = () => {
    dispatch(setMode({ modeType: 'Boy' }));
  };
  
  const handleGirlMode = () => {
    dispatch(setMode({ modeType: 'Girl' }));
  };

  const handleLogout = async () => {
    try {
      await logout();
      Alert.alert('Success', 'You have been logged out.');
      navigation.replace('Login'); 
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
        {status === 'loading' && <Text>Yükleniyor...</Text>}
        {error && <Text style={{ color: 'red' }}>{error}</Text>}
        <Button title="Logout" onPress={handleLogout} />
      </SafeAreaView>
    </Background>
  );
};

export default Settings;
