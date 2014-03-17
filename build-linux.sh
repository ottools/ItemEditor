#!/bin/sh

FINAL_RELEASE_DIR="`pwd`/release"
PLUGIN_XML_DIR="`pwd`/Source/Plug-ins"
RELEASE_DIR="`pwd`/Source/bin/Release"

xbuild /p:Configuration=Release ItemEditor.sln

rm -rf $FINAL_RELEASE_DIR
mkdir -p $FINAL_RELEASE_DIR || exit
mkdir -p $FINAL_RELEASE_DIR/Plug-ins
cp $RELEASE_DIR/*.exe $FINAL_RELEASE_DIR
cp $RELEASE_DIR/ImageSimilarity.dll $FINAL_RELEASE_DIR
cp $RELEASE_DIR/PluginInterface.dll $FINAL_RELEASE_DIR
cp $RELEASE_DIR/Plugin*.dll $FINAL_RELEASE_DIR/Plug-ins
cp $PLUGIN_XML_DIR/*.xml $FINAL_RELEASE_DIR/Plug-ins
