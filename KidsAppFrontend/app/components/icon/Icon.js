import { View, Text, Image } from 'react-native'
import React from 'react'
import styles from './styles'

const Icon = () => {
  return (
    <View style={styles.container}>
      <Image source={require('@/app/assets/icon/boyIcon.png')} style={styles.icon} />
    </View>
  )
}

export default Icon