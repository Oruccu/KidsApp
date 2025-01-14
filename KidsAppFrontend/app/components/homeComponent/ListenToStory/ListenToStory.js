import { View, Text, Image, TouchableOpacity } from 'react-native'
import React from 'react'
import styles from './styles'

const ListenToStory = ({ Icon, Title, onPress , url}) => {

    
  return (
    <TouchableOpacity onPress={onPress}>
    <View style={styles.container}>
      <Image source={url} style={styles.image} />
      <Text style={styles.title} numberOfLines={2}>{Title}</Text>
    </View>
  </TouchableOpacity>
  )
}

export default ListenToStory

