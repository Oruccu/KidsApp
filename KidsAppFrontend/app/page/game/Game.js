import { View, Text, SafeAreaView } from 'react-native'
import React from 'react'
import styles from './styles'
import Banner from '@/app/components/Banner'
const Game = () => {
  return (
    <SafeAreaView>
      <Banner/>
      <View>
        <Text>Game</Text>
      </View>
    </SafeAreaView>
  )
}

export default Game