import { View, Text, SafeAreaView } from 'react-native'
import React from 'react'
import Background from '@/app/components/background'
import styles from './styles'
import Banner from '@/app/components/Banner'
import SettingsButton from '@/app/components/settingsComponents/SettingsButton'
const Settings = () => {
  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>
        <Banner bannerTitle={'Ayşe Nur'}/>
        <View>

        </View>
      </SafeAreaView>
    </Background>
  )
}

export default Settings