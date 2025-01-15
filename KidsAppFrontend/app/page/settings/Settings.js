import { View, Text, SafeAreaView } from 'react-native'
import React from 'react'
import Background from '@/app/components/background'
import styles from './styles'
import Banner from '@/app/components/homeComponent/Banner'

const Settings = () => {
  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>
        <View>
          <Text>Settings</Text>
        </View>
      </SafeAreaView>
    </Background>
  )
}

export default Settings