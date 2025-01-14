import { View, Text, SafeAreaView } from 'react-native'
import React from 'react'
import Background from '@/app/components/background'
import styles from './styles'
const Home = () => {
  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>

        <Text>Home</Text>
      </SafeAreaView>
    </Background>
  )
}

export default Home