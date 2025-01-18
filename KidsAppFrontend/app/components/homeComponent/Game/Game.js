import React from 'react';
import { View, Text, Image, TouchableOpacity, StyleSheet, FlatList } from 'react-native';
import { useSelector } from 'react-redux';
import { RootState } from '@/app/store/store';
import color from '@/app/styles/color';

const GameBox = ({ imageSource, title, onPress }) => {
  const currentMode = useSelector((state) => state.mode.currentMode);
  const boxStyle = [
    styles.boxContainer,
    {
      backgroundColor: currentMode === 'Boy' ? styles.colors.BoyColors.Primary : styles.colors.GirlColors.Primary,

    },
  ];

  return (
    <TouchableOpacity onPress={onPress} style={boxStyle}>
      <View style={styles.contentContainer}>
        <Image source={imageSource} style={styles.image} />
        <Text style={styles.text} numberOfLines={2}>{title}</Text>
      </View>
    </TouchableOpacity>
  );
};

const Game = () => {
  const data = [
    { id: '1', imageSource: require('@/app/assets/game/animals.png'), title: 'Learn Animals' },
    { id: '2', imageSource: require('@/app/assets/game/color.png'), title: 'Learn Color' },
    { id: '3', imageSource: require('@/app/assets/game/fruits.png'), title: 'Learn Fruit' },
    { id: '4', imageSource: require('@/app/assets/game/number.png'), title: 'Learn Number' },
  ];

  const renderItem = ({ item }) => (
    <GameBox imageSource={item.imageSource} title={item.title} onPress={() => console.log(item.title)} />
  );

  return (
    <FlatList
      data={data}
      renderItem={renderItem}
      keyExtractor={(item) => item.id}
      numColumns={2}
      contentContainerStyle={styles.flatListContainer}
      columnWrapperStyle={styles.row}
    />
  );
};

export default Game;

const styles = StyleSheet.create({
  flatListContainer: {
    padding: 10,
    justifyContent: 'center',
  },
  row: {
    flexDirection: 'row', // Ensures each row aligns items horizontally
    justifyContent: 'space-around',
  },
  boxContainer: {
    flex: 1,
    margin: 10,
    borderRadius: 30,
    padding: 10,
    height: 90,
    justifyContent:'center',
    alignItems:'center'
  },
  contentContainer: {
    flexDirection: 'row', // Image and text side by side
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  image: {
    width: 50,
    height: 50,
    resizeMode: 'contain',

  },
  text: {
    fontSize: 20,
    flexWrap: 'wrap',
    flex: 1,
    textAlign: 'center',
    color: color.GirlColors.Secondary
  },
  colors: color,
});
